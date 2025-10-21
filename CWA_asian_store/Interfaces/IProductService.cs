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

        Task DeleteAsync(int id);

    }
}