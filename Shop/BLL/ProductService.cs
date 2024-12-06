using DAL.Repositories;
using DAL.Repositories.Model;
using BLL.Services.Exceptions;
using BLL.Services.DTO;
using AutoMapper;

namespace BLL.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return _mapper.Map<List<ProductDTO>>(products);
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
