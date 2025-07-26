using System.ComponentModel.DataAnnotations;

namespace WeddingDressCMS.API.Models
{
    public class DressSize
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Size { get; set; } = string.Empty;
        
        public int Stock { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        // Navigation properties
        public int WeddingDressId { get; set; }
        public WeddingDress WeddingDress { get; set; } = null!;
    }
} 