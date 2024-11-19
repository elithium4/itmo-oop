namespace Lab3.Model
{
    public class Store: HasId
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
    }
}
