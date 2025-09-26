using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN232.Lab1.CoffeeStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__19093A0B273C077A", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__C99ED2303AC1AEF1", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__B40CC6CDE34F205E", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInMenu",
                columns: table => new
                {
                    ProductInMenuId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__BAC4646B0E681553", x => x.ProductInMenuId);
                    table.ForeignKey(
                        name: "FK_ProductInMenu_Menu",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInMenu_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Các loại cà phê", "Coffee" },
                    { 2, "Các loại trà", "Tea" },
                    { 3, "Bánh ngọt ăn kèm", "Pastry" }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "FromDate", "Name", "ToDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Morning Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Afternoon Menu", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Cà phê đậm đặc, vị mạnh", "Espresso", 30000m },
                    { 2, 1, "Cà phê sữa bọt", "Cappuccino", 40000m },
                    { 3, 1, "Cà phê sữa nóng", "Latte", 45000m },
                    { 4, 2, "Trà xanh truyền thống", "Green Tea", 25000m },
                    { 5, 2, "Trà đen nguyên chất", "Black Tea", 20000m },
                    { 6, 3, "Bánh sừng bò bơ", "Croissant", 35000m },
                    { 7, 3, "Bánh muffin socola", "Muffin", 30000m }
                });

            migrationBuilder.InsertData(
                table: "ProductInMenu",
                columns: new[] { "ProductInMenuId", "MenuId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 50 },
                    { 2, 1, 2, 50 },
                    { 3, 1, 4, 40 },
                    { 4, 1, 6, 20 },
                    { 5, 2, 3, 40 },
                    { 6, 2, 5, 30 },
                    { 7, 2, 7, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInMenu_MenuId",
                table: "ProductInMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "UQ_Product_Menu",
                table: "ProductInMenu",
                columns: new[] { "ProductId", "MenuId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInMenu");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
