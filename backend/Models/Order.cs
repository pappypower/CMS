using System.ComponentModel.DataAnnotations;

namespace WeddingDressCMS.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string CustomerEmail { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string BillingAddress { get; set; } = string.Empty;
        
        public decimal SubTotal { get; set; }
        
        public decimal Tax { get; set; }
        
        public decimal ShippingCost { get; set; }
        
        public decimal Total { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending";
        
        [StringLength(1000)]
        public string Notes { get; set; } = string.Empty;
        
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? ShippedDate { get; set; }
        
        public DateTime? DeliveredDate { get; set; }
        
        // Navigation properties
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
} 