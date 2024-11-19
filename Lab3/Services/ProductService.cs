using Lab3.Model;
using Lab3.Reposiories;

namespace Lab3.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) { _repository = repository; }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _repository.GetAllProductsAsync();
        }
    }
}
