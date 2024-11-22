using System.ComponentModel.DataAnnotations;

namespace Lab3.Repositories.Model
{
    public class StoreProduct
    {
        public string ProductName { get; set; }
        public int StoreId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public Store Store { get; set; }
        public Product Product { get; set; }

    }
}
