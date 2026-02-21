using AuthService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IUserRepo
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<bool> UpdateRoleAsync(int userId, string newRole);
        Task<bool> AddRoleAsync(string roleName);
        Task<List<UserDetailDto>> GetAllUsersAsync();
        Task<UserDetailDto?> GetUserByIdAsync(int id);
    }
}
