using Lab3.Model;

namespace Lab3.Reposiories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task AddProductToStoreAsync(int storeId, int productId, decimal price, int quantity);
        Task<IEnumerable<StoreProduct>> GetProductsInStoreAsync(int storeId);
        Task<StoreProduct> GetCheapestProductAsync(int productId);
        Task<IEnumerable<Store>> GetStoresWithProductsUnderPriceAsync(decimal price);
    }
}
