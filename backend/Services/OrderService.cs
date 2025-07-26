using Microsoft.EntityFrameworkCore;
using WeddingDressCMS.API.Data;
using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly WeddingDressContext _context;

        public OrderService(WeddingDressContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.WeddingDress)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.WeddingDress)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetOrderByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.WeddingDress)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Generate order number if not provided
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                order.OrderNumber = await GenerateOrderNumberAsync();
            }

            order.OrderDate = DateTime.UtcNow;
            
            // Calculate totals
            order.SubTotal = order.OrderItems.Sum(oi => oi.TotalPrice);
            order.Total = order.SubTotal + order.Tax + order.ShippingCost;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            return await GetOrderByIdAsync(order.Id) ?? order;
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            if (existingOrder == null)
                return null;

            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CustomerEmail = order.CustomerEmail;
            existingOrder.CustomerPhone = order.CustomerPhone;
            existingOrder.ShippingAddress = order.ShippingAddress;
            existingOrder.BillingAddress = order.BillingAddress;
            existingOrder.Status = order.Status;
            existingOrder.PaymentStatus = order.PaymentStatus;
            existingOrder.Notes = order.Notes;
            existingOrder.ShippedDate = order.ShippedDate;
            existingOrder.DeliveredDate = order.DeliveredDate;
            existingOrder.Tax = order.Tax;
            existingOrder.ShippingCost = order.ShippingCost;

            // Recalculate totals
            existingOrder.SubTotal = existingOrder.OrderItems.Sum(oi => oi.TotalPrice);
            existingOrder.Total = existingOrder.SubTotal + existingOrder.Tax + existingOrder.ShippingCost;

            await _context.SaveChangesAsync();
            
            return await GetOrderByIdAsync(id);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.WeddingDress)
                .Where(o => o.Status == status)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        private async Task<string> GenerateOrderNumberAsync()
        {
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var lastOrder = await _context.Orders
                .Where(o => o.OrderNumber.StartsWith($"WD{date}"))
                .OrderByDescending(o => o.OrderNumber)
                .FirstOrDefaultAsync();

            int sequence = 1;
            if (lastOrder != null)
            {
                var lastSequence = lastOrder.OrderNumber.Substring(10);
                if (int.TryParse(lastSequence, out var parsed))
                {
                    sequence = parsed + 1;
                }
            }

            return $"WD{date}{sequence:D4}";
        }
    }
} 