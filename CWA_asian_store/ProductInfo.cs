using CWA_asian_store.Entity.Model;

namespace CWA_asian_store
{
    public class ProductInfo
    {
        public Product Product { get; set; } = null!;
        public int TotalOrders { get; set; } // кількість проданих одиниць
    }
}



