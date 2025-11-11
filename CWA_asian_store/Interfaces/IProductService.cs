using CWA_asian_store.Data;
using CWA_asian_store.Entity.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CWA_asian_store.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);

        Task<List<Product>> SearchAsync(string? search, string? sortOrder);

        Task<List<Category>> GetAllCategoriesAsync();
        Task DeleteAsync(int id);

    }
}