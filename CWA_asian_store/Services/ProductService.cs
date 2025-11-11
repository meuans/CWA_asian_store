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

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchAsync(string? search, string? sortOrder)
        {
            var productsQuery = _db.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                string normalizedSearch = search.Trim();

                productsQuery = productsQuery.Where(p =>
                    EF.Functions.Like(p.Name, $"%{normalizedSearch}%"));
            }

            // Сортування
            productsQuery = sortOrder switch
            {
                "asc" => productsQuery.OrderBy(p => p.Price),
                "desc" => productsQuery.OrderByDescending(p => p.Price),
                _ => productsQuery.OrderBy(p => p.Id)
            };

            return await productsQuery.ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

    }
}