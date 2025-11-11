using System.ComponentModel.DataAnnotations.Schema;

namespace CWA_asian_store.Entity.Model
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

       

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
