using Microsoft.EntityFrameworkCore;
using Case.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Case.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.HasMany(u => u.ShoppingLists)
                      .WithOne(s => s.User)
                      .HasForeignKey(s => s.UserId);
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.ImageUrl).HasMaxLength(200);
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId);
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });


            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(s => s.User)
                      .WithMany(u => u.ShoppingLists)
                      .HasForeignKey(s => s.UserId);
            });


            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Note).HasMaxLength(200);
                entity.HasOne(s => s.ShoppingList)
                      .WithMany(sl => sl.Items)
                      .HasForeignKey(s => s.ShoppingListId);
                entity.HasOne(s => s.Product)
                      .WithMany()
                      .HasForeignKey(s => s.ProductId);
            });
        }
    }
}
