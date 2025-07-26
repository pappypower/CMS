using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(User user);
        string GenerateRefreshToken();
        Task<bool> ValidateTokenAsync(string token);
    }
} 