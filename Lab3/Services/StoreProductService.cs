using Lab3.Model;
using Lab3.Reposiories;

namespace Lab3.Services
{
    public class StoreProductService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;

        public StoreProductService(IStoreRepository storeRepository, IProductRepository productRepository)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
        }

        public async Task<List<(Product product, int quantity)>> GetProductsForAmountAsync(int storeId, int amount)
        {
            var storeProducts = await _productRepository.GetProductsInStoreAsync(storeId);
            var affordableProducts = new List<(Product product, int quantity)>();

            foreach (var storeProduct in storeProducts)
            {
                if (storeProduct.Price <= amount)
                {
                    int maxQuantity = (int)(amount / storeProduct.Price);
                    //var product = await _productRepository.GetProductByIdAsync(storeProduct.ProductId);
                    //affordableProducts.Add((product, maxQuantity));
                }
            }

            return affordableProducts;
        }
    }
}
