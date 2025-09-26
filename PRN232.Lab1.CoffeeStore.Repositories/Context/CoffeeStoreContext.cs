using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PRN232.Lab1.CoffeeStore.Repositories.Models;

namespace PRN232.Lab1.CoffeeStore.Repositories.Context;

public partial class CoffeeStoreContext : DbContext
{
    public CoffeeStoreContext()
    {
    }

    public CoffeeStoreContext(DbContextOptions<CoffeeStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductInMenu> ProductInMenus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B273C077A");

            entity.ToTable("Category");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED2303AC1AEF1");

            entity.ToTable("Menu");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDE34F205E");

            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<ProductInMenu>(entity =>
        {
            entity.HasKey(e => e.ProductInMenuId).HasName("PK__ProductI__BAC4646B0E681553");

            entity.ToTable("ProductInMenu");

            entity.HasIndex(e => e.MenuId, "IX_ProductInMenu_MenuId");

            entity.HasIndex(e => new { e.ProductId, e.MenuId }, "UQ_Product_Menu").IsUnique();

            entity.HasOne(d => d.Menu).WithMany(p => p.ProductInMenus)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_ProductInMenu_Menu");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInMenus)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductInMenu_Product");
        });

        // ====================== SEED DATA ======================
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Coffee", Description = "Các loại cà phê" },
            new Category { CategoryId = 2, Name = "Tea", Description = "Các loại trà" },
            new Category { CategoryId = 3, Name = "Pastry", Description = "Bánh ngọt ăn kèm" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Espresso", Price = 30000, Description = "Cà phê đậm đặc, vị mạnh", CategoryId = 1 },
            new Product { ProductId = 2, Name = "Cappuccino", Price = 40000, Description = "Cà phê sữa bọt", CategoryId = 1 },
            new Product { ProductId = 3, Name = "Latte", Price = 45000, Description = "Cà phê sữa nóng", CategoryId = 1 },
            new Product { ProductId = 4, Name = "Green Tea", Price = 25000, Description = "Trà xanh truyền thống", CategoryId = 2 },
            new Product { ProductId = 5, Name = "Black Tea", Price = 20000, Description = "Trà đen nguyên chất", CategoryId = 2 },
            new Product { ProductId = 6, Name = "Croissant", Price = 35000, Description = "Bánh sừng bò bơ", CategoryId = 3 },
            new Product { ProductId = 7, Name = "Muffin", Price = 30000, Description = "Bánh muffin socola", CategoryId = 3 }
        );

        modelBuilder.Entity<Menu>().HasData(
            new Menu
            {
                MenuId = 1,
                Name = "Morning Menu",
                FromDate = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                ToDate = new DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc)
            },
            new Menu
            {
                MenuId = 2,
                Name = "Afternoon Menu",
                FromDate = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                ToDate = new DateTime(2025, 12, 31, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<ProductInMenu>().HasData(
            new ProductInMenu { ProductInMenuId = 1, ProductId = 1, MenuId = 1, Quantity = 50 },
            new ProductInMenu { ProductInMenuId = 2, ProductId = 2, MenuId = 1, Quantity = 50 },
            new ProductInMenu { ProductInMenuId = 3, ProductId = 4, MenuId = 1, Quantity = 40 },
            new ProductInMenu { ProductInMenuId = 4, ProductId = 6, MenuId = 1, Quantity = 20 },
            new ProductInMenu { ProductInMenuId = 5, ProductId = 3, MenuId = 2, Quantity = 40 },
            new ProductInMenu { ProductInMenuId = 6, ProductId = 5, MenuId = 2, Quantity = 30 },
            new ProductInMenu { ProductInMenuId = 7, ProductId = 7, MenuId = 2, Quantity = 25 }
        );

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
