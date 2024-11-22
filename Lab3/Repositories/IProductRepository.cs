using Lab3.Repositories.Model;

namespace Lab3.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByNameAsync(string name);
        Task<List<ProductStoreDetail>> GetProductInAllStores(string name);

        Task<List<ProductStoreDetail>> GetProductsByStoreIdAsync(int id);
        Task<ProductStoreDetail> GetProductInStoreAsync(int storeId, string productName);
        Task RemoveProductFromStoreAsync(ProductStoreDetail entity);
        Task AddOrUpdateProductInStore(ProductStoreDetail entity);
    }
}
