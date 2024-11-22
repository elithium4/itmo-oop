using Lab3.Repositories;
using Lab3.Repositories.Model;
using Lab3.Services.Exceptions;

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

        public async Task CreateProduct(string name)
        {
            var existingProduct = await _repository.GetProductByNameAsync(name);
            if (existingProduct != null)
            {
                throw new ProductAlreadyExistsException(name);
            }

            var newProduct = new Product { Name = name };
            await _repository.CreateProductAsync(newProduct);

        }

    }
}
