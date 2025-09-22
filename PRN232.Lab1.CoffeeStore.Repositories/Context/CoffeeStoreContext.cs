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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
