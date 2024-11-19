using Lab3.Repositories;
using Lab3.Repositories.Model;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Lab3.Repositories.File
{
    public class FileProductRepository : IProductRepository
    {
        private readonly string _filePath;

        public FileProductRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task CreateProductAsync(Product product)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            var newLine = $"{product.Name}";
            await System.IO.File.AppendAllLinesAsync(_filePath, [newLine]);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            return lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Product
                {
                    Name = parts[1],
                };
            }).ToList();
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            string pattern = $@"^{Regex.Escape(name)},";
            Regex regex = new Regex(pattern);
            var line = lines.FirstOrDefault(l => regex.IsMatch(l));
            if (line == null) return null;

            var parts = line.Split(',');
            return new Product
            {
                Name = parts[1],
            };
        }

        public async Task<List<ProductStoreDetail>> GetProductInAllStores(string name)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            string pattern = $@"^{Regex.Escape(name)},";
            Regex regex = new Regex(pattern);
            var line = lines.FirstOrDefault(l => regex.IsMatch(l));
            if (line == null) return null;

            var parts = line.Split(',');
            List<ProductStoreDetail> productInStores = new List<ProductStoreDetail>();
            for (int i = 1; i < parts.Length; i += 3)
            {
                productInStores.Add(new ProductStoreDetail
                {
                    ProductName = name,
                    StoreId = int.Parse(parts[i]),
                    Amount = int.Parse(parts[i + 1]),
                    Price = int.Parse(parts[i + 2])
                });
            }
            return productInStores;
        }

        public async Task<List<ProductStoreDetail>> GetProductsByStoreIdAsync(int id)
        {
            var matchingProducts = new List<ProductStoreDetail>();

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var parts = line.Split(',');

                    for (int i = 1; i < parts.Length; i += 3)
                    {
                        if (parts[i] == id.ToString())
                        {
                            matchingProducts.Add(
                                new ProductStoreDetail{
                                    ProductName = parts[0],
                                    StoreId = id,
                                    Amount = int.Parse(parts[i+1]),
                                    Price = double.Parse(parts[i+2]),
                                });
                            break;
                        }
                    }
                }
            }

            return matchingProducts;
        }
    }
}
