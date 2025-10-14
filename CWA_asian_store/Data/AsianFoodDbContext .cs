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
    }
}


