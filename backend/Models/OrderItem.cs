using System.ComponentModel.DataAnnotations;

namespace WeddingDressCMS.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        [StringLength(10)]
        public string Size { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string SpecialInstructions { get; set; } = string.Empty;
        
        // Navigation properties
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        
        public int WeddingDressId { get; set; }
        public WeddingDress WeddingDress { get; set; } = null!;
    }
} 