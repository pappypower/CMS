using Microsoft.EntityFrameworkCore;
using WeddingDressCMS.API.Data;
using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly WeddingDressContext _context;

        public CategoryService(WeddingDressContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.WeddingDresses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.ImageUrl = category.ImageUrl;
            existingCategory.IsActive = category.IsActive;
            existingCategory.SortOrder = category.SortOrder;

            await _context.SaveChangesAsync();
            
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            // Check if category has dresses
            var hasDresses = await _context.WeddingDresses.AnyAsync(d => d.CategoryId == id);
            if (hasDresses)
            {
                // Soft delete by marking as inactive
                category.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hard delete if no dresses are associated
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            
            return true;
        }
    }
} 