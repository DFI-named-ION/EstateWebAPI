using System;
using System.Collections.Generic;
using EstateWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EstateWebAPI.EF;

public partial class EstateWebApiContext : DbContext
{
    public EstateWebApiContext()
    {
    }

    public EstateWebApiContext(DbContextOptions<EstateWebApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Estate> Estates { get; set; }

    public virtual DbSet<EstateComment> EstateComments { get; set; }

    public virtual DbSet<EstateLike> EstateLikes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_id_primary");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Estate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estates_id_primary");

            entity.ToTable("estates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.FloorCount).HasColumnName("floor_count");
            entity.Property(e => e.Image)
                .HasMaxLength(512)
                .HasColumnName("image");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RoomCount).HasColumnName("room_count");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Estates)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estates_category_id_foreign");

            entity.HasOne(d => d.Owner).WithMany(p => p.Estates)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estates_owner_id_foreign");
        });

        modelBuilder.Entity<EstateComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estate_comments_id_primary");

            entity.ToTable("estate_comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstateId).HasColumnName("estate_id");
            entity.Property(e => e.Text)
                .HasMaxLength(1024)
                .HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Estate).WithMany(p => p.EstateComments)
                .HasForeignKey(d => d.EstateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estate_comments_estate_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.EstateComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estate_comments_user_id_foreign");
        });

        modelBuilder.Entity<EstateLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("estate_likes_id_primary");

            entity.ToTable("estate_likes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstateId).HasColumnName("estate_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Estate).WithMany(p => p.EstateLikes)
                .HasForeignKey(d => d.EstateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estate_likes_estate_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.EstateLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estate_likes_user_id_foreign");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_id_primary");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_user_id_foreign");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_items_id_primary");

            entity.ToTable("order_items");

            entity.HasIndex(e => e.EstateId, "order_items_estate_id_index");

            entity.HasIndex(e => e.OrderId, "order_items_order_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.EstateId).HasColumnName("estate_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Estate).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.EstateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_estate_id_foreign");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_order_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_id_primary");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_name_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_id_primary");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_index");

            entity.HasIndex(e => e.Password, "users_password_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
