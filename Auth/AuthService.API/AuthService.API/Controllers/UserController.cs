using AuthService.Application.DTO;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo repo;
        public UserController(IUserRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Register(RegisterRequest request)
        {
            var response = await repo.RegisterAsync(request);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Registration successful"));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login(LoginRequest request)
        {
            var response = await repo.LoginAsync(request);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Login successful"));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken(RefreshTokenRequest request)
        {
            var response = await repo.RefreshTokenAsync(request.Token);
            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Token refreshed successfully"));
        }

        [HttpPost("revoke-token")]
        public async Task<ActionResult<ApiResponse>> RevokeToken(RefreshTokenRequest request)
        {
            if (!await repo.RevokeTokenAsync(request.Token))
                return NotFound(ApiResponse.ErrorResponse("Token not found or already revoked."));
            
            return Ok(ApiResponse.SuccessMessage("Token revoked successfully."));
        }

        [HttpPut("update-role")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> UpdateRole(UpdateRoleRequest request)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value; 
            if (roleClaim != "Admin")
                return Forbid();

            if (!await repo.UpdateRoleAsync(request.UserId, request.NewRole))
                return NotFound(ApiResponse.ErrorResponse("User not found."));
            
            return Ok(ApiResponse.SuccessMessage("User role updated successfully."));
        }

        [HttpPost("role")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> CreateRole(CreateRoleRequest request)
        {
            var roleClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
            if (roleClaim != "Admin")
                return Forbid();
            if (await repo.AddRoleAsync(request.RoleName))
                return Ok(ApiResponse.SuccessMessage("Role created successfully."));

            return BadRequest(ApiResponse.ErrorResponse("Role already exists."));
        }

        [HttpGet]
        
        public async Task<ActionResult<ApiResponse<List<UserDetailDto>>>> GetAllUsers()
        {
            var users = await repo.GetAllUsersAsync();
            return Ok(ApiResponse<List<UserDetailDto>>.SuccessResponse(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserDetailDto>>> GetUserById(int id)
        {
            var user = await repo.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(ApiResponse<UserDetailDto>.ErrorResponse("User not found."));

            return Ok(ApiResponse<UserDetailDto>.SuccessResponse(user));
        }
    }
}
