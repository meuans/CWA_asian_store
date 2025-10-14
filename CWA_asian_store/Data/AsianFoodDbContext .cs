using CWA_asian_store.Entity.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CWA_asian_store.Data
{
    public class AsianFoodDbContext : DbContext
    {
        public AsianFoodDbContext(DbContextOptions<AsianFoodDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2); // до 99999999.99

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasPrecision(10, 2);

            // Виклик seed-даних
             new DbInitializer(modelBuilder).Seed();
        }

    }
}



