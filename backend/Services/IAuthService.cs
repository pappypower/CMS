using WeddingDressCMS.API.Models.Auth;

namespace WeddingDressCMS.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<UserInfo> GetUserInfoAsync(string userId);
        Task<bool> LogoutAsync(string userId);
    }
} 