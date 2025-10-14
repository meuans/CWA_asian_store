using CWA_asian_store.Data;
using CWA_asian_store.Entity.Model;
using CWA_asian_store.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CWA_asian_store.Services
{
    public class ProductService : IProductService
    {
        private readonly AsianFoodDbContext _db;

        public ProductService(AsianFoodDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _db.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _db.Products.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }
    }
}