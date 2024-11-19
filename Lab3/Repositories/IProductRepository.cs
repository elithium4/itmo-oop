using Lab3.Repositories.Model;

namespace Lab3.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();
        //Task AddProductToStoreAsync(int storeId, int productId, decimal price, int quantity);
        //Task<IEnumerable<StoreProduct>> GetProductsInStoreAsync(int storeId);
        //Task<StoreProduct> GetCheapestProductAsync(int productId);
        //Task<IEnumerable<Store>> GetStoresWithProductsUnderPriceAsync(decimal price);

        Task<Product> GetProductByNameAsync(string name);
        Task<List<ProductStoreDetail>> GetProductInAllStores(string name);

        Task<List<ProductStoreDetail>> GetProductsByStoreIdAsync(int id);
    }
}
