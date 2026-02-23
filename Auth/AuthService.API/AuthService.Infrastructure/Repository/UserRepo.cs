using AuthService.Application.DTO;
using AuthService.Application.Interfaces;
using AuthService.Domain.Models;
using AuthService.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public UserRepo(ApplicationDbContext context, IConfiguration config, IMapper mapper)
        {
            this.db = context;
            this.config = config;
            this.mapper = mapper;
        }
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await db.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email is already registered.");

            var customerRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "customer");
            if (customerRole == null)
            {
                customerRole = new Role { Name = "customer" };
                db.Roles.Add(customerRole);
                await db.SaveChangesAsync();
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PassHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = customerRole.Id
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            await db.Entry(user).Reference(u => u.Role).LoadAsync();

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            db.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(GetRefreshTokenExpiryDays()),
                UserId = user.Id
            });
            await db.SaveChangesAsync();
            var response = mapper.Map<AuthResponse>(user);
            response.Token = accessToken;
            response.RefreshToken = refreshToken;
            return response;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PassHash))
                throw new Exception("Invalid email or password.");
            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            db.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(GetRefreshTokenExpiryDays()),
                UserId = user.Id
            });
            await db.SaveChangesAsync();
            var response = mapper.Map<AuthResponse>(user);
            response.Token = accessToken;
            response.RefreshToken = refreshToken;
            return response;
        }

        public async Task<AuthResponse> RefreshTokenAsync(string token)
        {
            var existingToken = await db.RefreshTokens
            .Include(rt => rt.User)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(rt => rt.Token == token);
            if (existingToken == null)
                throw new Exception("Invalid refresh token.");
            if (!existingToken.IsActive)
                throw new Exception("Refresh token is expired or revoked.");
            existingToken.RevokedAt = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            existingToken.ReplacedByToken = newRefreshToken;
            db.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(GetRefreshTokenExpiryDays()),
                UserId = existingToken.UserId
            });
            await db.SaveChangesAsync();
            var user = existingToken.User;
            var accessToken = GenerateJwtToken(user);
            var response = mapper.Map<AuthResponse>(user);
            response.Token = accessToken;
            response.RefreshToken = newRefreshToken;
            return response;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var existingToken = await db.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);
            if (existingToken == null || !existingToken.IsActive)
                return false;
            existingToken.RevokedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoleAsync(int userId, string newRole)
        {
            var user = await db.Users.FindAsync(userId);
            if (user == null)
                return false;

            var role = await db.Roles.FirstOrDefaultAsync(r => r.Name == newRole);
            if (role == null)
            {
                role = new Role { Name = newRole };
                db.Roles.Add(role);
                await db.SaveChangesAsync();
            }

            user.RoleId = role.Id;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRoleAsync(string roleName)
        {
            if (await db.Roles.AnyAsync(r => r.Name == roleName))
                return false;
            
            var role = new Role { Name = roleName };
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDetailDto>> GetAllUsersAsync()
        {
            var users = await db.Users.Include(u => u.Role).ToListAsync();
            return mapper.Map<List<UserDetailDto>>(users);
        }

        public async Task<UserDetailDto?> GetUserByIdAsync(int id)
        {
            var user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
            return user == null ? null : mapper.Map<UserDetailDto>(user);
        }


        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
            };
            var expiryMinutes = int.Parse(config["Jwt:AccessTokenExpiryMinutes"] ?? "15");
            var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        private int GetRefreshTokenExpiryDays()
        {
            return int.Parse(config["Jwt:RefreshTokenExpiryDays"] ?? "7");
        }
    }
}
