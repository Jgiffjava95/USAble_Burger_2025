using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using USAble_Burger_API.Models;

namespace USAble_Burger_API.DatabaseConnection;

public partial class UsableBurgerContext : DbContext
{
    public UsableBurgerContext()
    {
    }

    public UsableBurgerContext(DbContextOptions<UsableBurgerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<TaxType> TaxTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6D96E8C1500C");

            entity.Property(e => e.DiscountName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E838BE6E77BC7");

            entity.Property(e => e.ItemName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__ItemType__516F03B5C67FBF4A");

            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF7DE9B6B3");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED0681A256D126");

            entity.Property(e => e.OrderItemName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaxType>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("PK__TaxType__711BE0AC3EBE338D");

            entity.ToTable("TaxType");

            entity.Property(e => e.TaxName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C02EE9020");

            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
