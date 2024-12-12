using System.ComponentModel.DataAnnotations;

namespace DAL.Repositories.Model
{
    public class Product
    {
        [Key]
        public string Name { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
    }
}
