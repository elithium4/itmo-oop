using System.ComponentModel.DataAnnotations;

namespace DAL.Repositories.Model
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
    }
}
