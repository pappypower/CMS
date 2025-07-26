using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Services
{
    public interface IDressService
    {
        Task<IEnumerable<WeddingDress>> GetAllDressesAsync();
        Task<WeddingDress?> GetDressByIdAsync(int id);
        Task<IEnumerable<WeddingDress>> GetDressesByCategoryAsync(int categoryId);
        Task<IEnumerable<WeddingDress>> GetFeaturedDressesAsync();
        Task<WeddingDress> CreateDressAsync(WeddingDress dress);
        Task<WeddingDress?> UpdateDressAsync(int id, WeddingDress dress);
        Task<bool> DeleteDressAsync(int id);
        Task<IEnumerable<WeddingDress>> SearchDressesAsync(string searchTerm);
    }
} 