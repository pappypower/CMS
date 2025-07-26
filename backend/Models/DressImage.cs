using System.ComponentModel.DataAnnotations;

namespace WeddingDressCMS.API.Models
{
    public class DressImage
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string AltText { get; set; } = string.Empty;
        
        public bool IsPrimary { get; set; } = false;
        
        public int SortOrder { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public int WeddingDressId { get; set; }
        public WeddingDress WeddingDress { get; set; } = null!;
    }
} 