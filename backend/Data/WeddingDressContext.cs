using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingDressCMS.API.Models;

namespace WeddingDressCMS.API.Data
{
    public class WeddingDressContext : IdentityDbContext<User>
    {
        public WeddingDressContext(DbContextOptions<WeddingDressContext> options) : base(options)
        {
        }

        public DbSet<WeddingDress> WeddingDresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DressImage> DressImages { get; set; }
        public DbSet<DressSize> DressSizes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision
            modelBuilder.Entity<WeddingDress>()
                .Property(e => e.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<WeddingDress>()
                .Property(e => e.SalePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.SubTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Tax)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.ShippingCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 2);

            // Configure relationships
            modelBuilder.Entity<WeddingDress>()
                .HasOne(d => d.Category)
                .WithMany(c => c.WeddingDresses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DressImage>()
                .HasOne(i => i.WeddingDress)
                .WithMany(d => d.Images)
                .HasForeignKey(i => i.WeddingDressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DressSize>()
                .HasOne(s => s.WeddingDress)
                .WithMany(d => d.Sizes)
                .HasForeignKey(s => s.WeddingDressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.WeddingDress)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.WeddingDressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            modelBuilder.Entity<WeddingDress>()
                .HasIndex(d => d.SKU)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "A-Line", Description = "Classic A-line wedding dresses", SortOrder = 1 },
                new Category { Id = 2, Name = "Mermaid", Description = "Elegant mermaid style dresses", SortOrder = 2 },
                new Category { Id = 3, Name = "Ball Gown", Description = "Traditional ball gown wedding dresses", SortOrder = 3 },
                new Category { Id = 4, Name = "Sheath", Description = "Modern sheath wedding dresses", SortOrder = 4 },
                new Category { Id = 5, Name = "Bohemian", Description = "Boho and free-spirited wedding dresses", SortOrder = 5 }
            );

            // Seed Wedding Dresses
            modelBuilder.Entity<WeddingDress>().HasData(
                new WeddingDress 
                { 
                    Id = 1, 
                    Name = "Enchanted Dreams A-Line", 
                    Description = "A stunning A-line dress with intricate lace details and a flowing train.",
                    Price = 1299.99m,
                    SKU = "WD001",
                    Stock = 5,
                    Designer = "Elegance Bridal",
                    Style = "Classic",
                    Silhouette = "A-Line",
                    Neckline = "V-Neck",
                    SleeveStyle = "Long Sleeves",
                    Color = "Ivory",
                    Fabric = "Lace, Tulle",
                    TrainStyle = "Chapel Train",
                    CategoryId = 1,
                    IsAvailable = true,
                    IsFeatured = true
                },
                new WeddingDress 
                { 
                    Id = 2, 
                    Name = "Royal Elegance Mermaid", 
                    Description = "A sophisticated mermaid dress that hugs your curves perfectly.",
                    Price = 1899.99m,
                    SKU = "WD002",
                    Stock = 3,
                    Designer = "Royal Couture",
                    Style = "Modern",
                    Silhouette = "Mermaid",
                    Neckline = "Sweetheart",
                    SleeveStyle = "Strapless",
                    Color = "White",
                    Fabric = "Satin, Lace",
                    TrainStyle = "Court Train",
                    CategoryId = 2,
                    IsAvailable = true,
                    IsFeatured = true
                }
            );
        }
    }
} 