using CWA_asian_store.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace CWA_asian_store.Data
{
    public class DbInitializer
    {
        
      private readonly ModelBuilder modelBuilder;

            public DbInitializer(ModelBuilder modelBuilder)
            {
                this.modelBuilder = modelBuilder;
            }
   

        public void Seed()
            {
                // Додаємо продукти
                modelBuilder.Entity<Product>().HasData(
                    new Product { Id = 1, Name = "Рамен", Description = "Японський суп з локшиною", Price = 180, Category = "Японська кухня" },
                    new Product { Id = 2, Name = "Кімчі", Description = "Корейська гостра капуста", Price = 120, Category = "Корейська кухня" },
                    new Product { Id = 3, Name = "Пад Тай", Description = "Тайська локшина з куркою", Price = 210, Category = "Тайська кухня" },
                    new Product { Id = 4, Name = "Суші-сет", Description = "Асорті ролів та суші", Price = 350, Category = "Японська кухня" }
                );

                // Додаємо покупців
                modelBuilder.Entity<Customer>().HasData(
                   new Customer { Id = 1, FullName = "Анна Іваненко", Email = "anna@example.com", Address = "м. Київ, вул. Хрещатик, 10" },
                   new Customer { Id = 2, FullName = "Олег Петров", Email = "oleg@example.com", Address = "м. Львів, вул. Шевченка, 25" }
                );

                // Додаємо замовлення
                modelBuilder.Entity<Order>().HasData(
                    new Order { Id = 1, CustomerId = 1, CreatedAt = new DateTime(2025, 10, 10), Total = 480 },
                    new Order { Id = 2, CustomerId = 2, CreatedAt = new DateTime(2025, 10, 11), Total = 350 }
                );

                // Додаємо елементи замовлень
                modelBuilder.Entity<OrderItem>().HasData(
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, Price = 180 },
                    new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1, Price = 120 },
                    new OrderItem { Id = 3, OrderId = 2, ProductId = 4, Quantity = 1, Price = 350 }
                );
            }
        
    }
}




