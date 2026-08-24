using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class CybersoftMarketPlaceContext : DbContext
{
    public CybersoftMarketPlaceContext()
    {
    }

    public CybersoftMarketPlaceContext(DbContextOptions<CybersoftMarketPlaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customer1> Customer1s { get; set; }

    public virtual DbSet<GetAllProductDetailsViewDemo> GetAllProductDetailsViewDemos { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<VGetAllProductsDetail> VGetAllProductsDetails { get; set; }

    public virtual DbSet<VGetAllProductsDetailV> VGetAllProductsDetailVs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DBConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carts_Users");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.VariantId });

            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItems_Carts");

            entity.HasOne(d => d.Variant).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItems_ProductVariants");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Shop).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK_Categories_Shops");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Buyer).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversations_Users");

            entity.HasOne(d => d.Shop).WithMany(p => p.Conversations)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conversations_Shops");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3213E83F7F9AFA36");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .HasColumnName("alias");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3213E83F84B7ED4B");

            entity.ToTable("CUSTOMER1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .HasColumnName("alias");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DateCreate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<GetAllProductDetailsViewDemo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetAllProductDetailsViewDemo");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.ProductVariantName).HasMaxLength(200);
            entity.Property(e => e.ShopName).HasMaxLength(200);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Conversations");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Users");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.VariantId });

            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_Orders");

            entity.HasOne(d => d.Variant).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.VariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_ProductVariants");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.DisplayPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Shop).WithMany(p => p.Products)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Shops");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImages_Products");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VariantName).HasMaxLength(200);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariants_Products");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(d => d.Order).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ratings_Products");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Image).HasMaxLength(200);
            entity.Property(e => e.ShopName).HasMaxLength(200);

            entity.HasOne(d => d.Owner).WithMany(p => p.Shops)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shops_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07548A84E6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Alias).HasMaxLength(200);
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deleted).HasDefaultValue(false);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.Property(e => e.Desc)
                .HasMaxLength(255)
                .HasColumnName("desc");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<VGetAllProductsDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_GetAllProductsDetail");

            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.ProductVariantName).HasMaxLength(200);
            entity.Property(e => e.ShopName).HasMaxLength(200);
        });

        modelBuilder.Entity<VGetAllProductsDetailV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_getAllProductsDetail_v");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.ProductVariantName).HasMaxLength(200);
            entity.Property(e => e.TenShop).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
