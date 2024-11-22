using Lab3.Repositories.Model;

namespace Lab3.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string name);
        Task<List<StoreProduct>> GetProductInAllStores(string name);

        Task<List<StoreProduct>> GetProductsByStoreIdAsync(int id);
        Task<StoreProduct> GetProductInStoreAsync(int storeId, string productName);
        Task RemoveProductFromStoreAsync(StoreProduct entity);
        Task AddOrUpdateProductInStore(StoreProduct entity);
    }
}
