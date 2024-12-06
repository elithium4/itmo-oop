using DAL.Repositories.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.SQL
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

        public async Task<List<StoreProduct>> GetProductInAllStores(string name)
        {
            return await _context.ProductStoreDetails.Where(p => p.ProductName == name).ToListAsync();
        }

        public async Task<List<StoreProduct>> GetProductsByStoreIdAsync(int id)
        {
            return await _context.ProductStoreDetails.Where(p => p.StoreId == id).ToListAsync();
        }

        public async Task AddOrUpdateProductInStore(StoreProduct entity)
        {
            var existingDetail = _context.ProductStoreDetails.Find(entity.StoreId, entity.ProductName);
            if (existingDetail == null) {
             _context.ProductStoreDetails.Add(entity);
            } else {
             _context.ProductStoreDetails.Update(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<StoreProduct> GetProductInStoreAsync(int storeId, string productName)
        {
            return await _context.ProductStoreDetails.FindAsync(storeId, productName);
        }

        public async Task RemoveProductFromStoreAsync(StoreProduct entity)
        {
             _context.ProductStoreDetails.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
