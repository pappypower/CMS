using Microsoft.EntityFrameworkCore;
using WeddingDressCMS.API.Data;
using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Services
{
    public class DressService : IDressService
    {
        private readonly WeddingDressContext _context;

        public DressService(WeddingDressContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeddingDress>> GetAllDressesAsync()
        {
            return await _context.WeddingDresses
                .Include(d => d.Category)
                .Include(d => d.Images)
                .Include(d => d.Sizes)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<WeddingDress?> GetDressByIdAsync(int id)
        {
            return await _context.WeddingDresses
                .Include(d => d.Category)
                .Include(d => d.Images)
                .Include(d => d.Sizes)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<WeddingDress>> GetDressesByCategoryAsync(int categoryId)
        {
            return await _context.WeddingDresses
                .Include(d => d.Category)
                .Include(d => d.Images)
                .Include(d => d.Sizes)
                .Where(d => d.CategoryId == categoryId && d.IsAvailable)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<WeddingDress>> GetFeaturedDressesAsync()
        {
            return await _context.WeddingDresses
                .Include(d => d.Category)
                .Include(d => d.Images)
                .Include(d => d.Sizes)
                .Where(d => d.IsFeatured && d.IsAvailable)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<WeddingDress> CreateDressAsync(WeddingDress dress)
        {
            dress.CreatedAt = DateTime.UtcNow;
            dress.UpdatedAt = DateTime.UtcNow;
            
            _context.WeddingDresses.Add(dress);
            await _context.SaveChangesAsync();
            
            return await GetDressByIdAsync(dress.Id) ?? dress;
        }

        public async Task<WeddingDress?> UpdateDressAsync(int id, WeddingDress dress)
        {
            var existingDress = await _context.WeddingDresses.FindAsync(id);
            if (existingDress == null)
                return null;

            existingDress.Name = dress.Name;
            existingDress.Description = dress.Description;
            existingDress.Price = dress.Price;
            existingDress.SalePrice = dress.SalePrice;
            existingDress.SKU = dress.SKU;
            existingDress.Stock = dress.Stock;
            existingDress.Designer = dress.Designer;
            existingDress.Style = dress.Style;
            existingDress.Silhouette = dress.Silhouette;
            existingDress.Neckline = dress.Neckline;
            existingDress.SleeveStyle = dress.SleeveStyle;
            existingDress.Color = dress.Color;
            existingDress.Fabric = dress.Fabric;
            existingDress.TrainStyle = dress.TrainStyle;
            existingDress.IsAvailable = dress.IsAvailable;
            existingDress.IsFeatured = dress.IsFeatured;
            existingDress.CategoryId = dress.CategoryId;
            existingDress.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            return await GetDressByIdAsync(id);
        }

        public async Task<bool> DeleteDressAsync(int id)
        {
            var dress = await _context.WeddingDresses.FindAsync(id);
            if (dress == null)
                return false;

            _context.WeddingDresses.Remove(dress);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WeddingDress>> SearchDressesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllDressesAsync();

            searchTerm = searchTerm.ToLower();

            return await _context.WeddingDresses
                .Include(d => d.Category)
                .Include(d => d.Images)
                .Include(d => d.Sizes)
                .Where(d => d.IsAvailable && (
                    d.Name.ToLower().Contains(searchTerm) ||
                    d.Description.ToLower().Contains(searchTerm) ||
                    d.Designer.ToLower().Contains(searchTerm) ||
                    d.Style.ToLower().Contains(searchTerm) ||
                    d.Color.ToLower().Contains(searchTerm) ||
                    d.Category.Name.ToLower().Contains(searchTerm)
                ))
                .OrderBy(d => d.Name)
                .ToListAsync();
        }
    }
} 