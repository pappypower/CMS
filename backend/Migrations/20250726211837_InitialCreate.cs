using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeddingDressCMS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeddingDresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Designer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Style = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Silhouette = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neckline = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SleeveStyle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fabric = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainStyle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingDresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeddingDresses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DressImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeddingDressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DressImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DressImages_WeddingDresses_WeddingDressId",
                        column: x => x.WeddingDressId,
                        principalTable: "WeddingDresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DressSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    WeddingDressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DressSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DressSizes_WeddingDresses_WeddingDressId",
                        column: x => x.WeddingDressId,
                        principalTable: "WeddingDresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    WeddingDressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_WeddingDresses_WeddingDressId",
                        column: x => x.WeddingDressId,
                        principalTable: "WeddingDresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5291), "Classic A-line wedding dresses", "", true, "A-Line", 1 },
                    { 2, new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5299), "Elegant mermaid style dresses", "", true, "Mermaid", 2 },
                    { 3, new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5300), "Traditional ball gown wedding dresses", "", true, "Ball Gown", 3 },
                    { 4, new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5301), "Modern sheath wedding dresses", "", true, "Sheath", 4 },
                    { 5, new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5302), "Boho and free-spirited wedding dresses", "", true, "Bohemian", 5 }
                });

            migrationBuilder.InsertData(
                table: "WeddingDresses",
                columns: new[] { "Id", "CategoryId", "Color", "CreatedAt", "Description", "Designer", "Fabric", "IsAvailable", "IsFeatured", "Name", "Neckline", "Price", "SKU", "SalePrice", "Silhouette", "SleeveStyle", "Stock", "Style", "TrainStyle", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Ivory", new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5506), "A stunning A-line dress with intricate lace details and a flowing train.", "Elegance Bridal", "Lace, Tulle", true, true, "Enchanted Dreams A-Line", "V-Neck", 1299.99m, "WD001", null, "A-Line", "Long Sleeves", 5, "Classic", "Chapel Train", new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5506) },
                    { 2, 2, "White", new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5521), "A sophisticated mermaid dress that hugs your curves perfectly.", "Royal Couture", "Satin, Lace", true, true, "Royal Elegance Mermaid", "Sweetheart", 1899.99m, "WD002", null, "Mermaid", "Strapless", 3, "Modern", "Court Train", new DateTime(2025, 7, 26, 21, 18, 35, 942, DateTimeKind.Utc).AddTicks(5521) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DressImages_WeddingDressId",
                table: "DressImages",
                column: "WeddingDressId");

            migrationBuilder.CreateIndex(
                name: "IX_DressSizes_WeddingDressId",
                table: "DressSizes",
                column: "WeddingDressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_WeddingDressId",
                table: "OrderItems",
                column: "WeddingDressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeddingDresses_CategoryId",
                table: "WeddingDresses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingDresses_SKU",
                table: "WeddingDresses",
                column: "SKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DressImages");

            migrationBuilder.DropTable(
                name: "DressSizes");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WeddingDresses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
