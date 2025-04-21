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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RI52K2C;Database=USAble_Burger;Trusted_Connection=True;TrustServerCertificate=true"); //WTF

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6D9633EA945B");

            entity.Property(e => e.DiscountName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E838BD79135C0");

            entity.Property(e => e.ItemName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__ItemType__516F03B5D623A109");

            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFDA8A15AC");

            entity.Property(e => e.OrderItems)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C51DE57B2");

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
