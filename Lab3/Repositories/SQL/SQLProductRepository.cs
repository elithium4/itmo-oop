using Lab3.Repositories.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Repositories.SQL
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;


        public SQLProductRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Products
                             .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<ProductStoreDetail>> GetProductInAllStores(string name)
        {
            return await _context.ProductStoreDetails.Where(p => p.ProductName == name).ToListAsync();
        }

        public async Task<List<ProductStoreDetail>> GetProductsByStoreIdAsync(int id)
        {
            return await _context.ProductStoreDetails.Where(p => p.StoreId == id).ToListAsync();
        }

        public async Task AddOrUpdateProductInStore(ProductStoreDetail entity)
        {
            var existingDetail = _context.ProductStoreDetails.Find(entity.StoreId, entity.ProductName);
            if (existingDetail == null) {
             _context.ProductStoreDetails.Add(entity);
            } else {
             _context.ProductStoreDetails.Update(entity);
            }
            await _context.SaveChangesAsync();
        }
    }
}
