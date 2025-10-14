using System.ComponentModel.DataAnnotations.Schema;

namespace CWA_asian_store.Entity.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();

        public decimal Total { get; set; }
    }
}
