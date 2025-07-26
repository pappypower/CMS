using System.ComponentModel.DataAnnotations;

namespace WeddingDressCMS.API.Models
{
    public class WeddingDress
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public decimal Price { get; set; }
        
        public decimal? SalePrice { get; set; }
        
        [Required]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;
        
        public int Stock { get; set; }
        
        [StringLength(50)]
        public string Designer { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Style { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Silhouette { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Neckline { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string SleeveStyle { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Color { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Fabric { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string TrainStyle { get; set; } = string.Empty;
        
        public bool IsAvailable { get; set; } = true;
        
        public bool IsFeatured { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        public ICollection<DressImage> Images { get; set; } = new List<DressImage>();
        
        public ICollection<DressSize> Sizes { get; set; } = new List<DressSize>();
        
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
} 