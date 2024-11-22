﻿using Lab3.Repositories;
using Lab3.Repositories.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<StoreProduct>> GetProductInAllStores(string name)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            string pattern = $@"^{Regex.Escape(name)},";
            Regex regex = new Regex(pattern);
            var line = lines.FirstOrDefault(l => regex.IsMatch(l));
            if (line == null) return null;

            var parts = line.Split(',');
            List<StoreProduct> productInStores = new List<StoreProduct>();
            for (int i = 1; i < parts.Length; i += 3)
            {
                productInStores.Add(new StoreProduct
                {
                    ProductName = name,
                    StoreId = int.Parse(parts[i]),
                    Amount = int.Parse(parts[i + 1]),
                    Price = int.Parse(parts[i + 2])
                });
            }
            return productInStores;
        }

        public async Task<List<StoreProduct>> GetProductsByStoreIdAsync(int id)
        {
            var matchingProducts = new List<StoreProduct>();

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
                                new StoreProduct{
                                    ProductName = parts[0],
                                    StoreId = id,
                                    Amount = int.Parse(parts[i+1]),
                                    Price = int.Parse(parts[i+2]),
                                });
                            break;
                        }
                    }
                }
            }

            return matchingProducts;
        }

        public async Task AddOrUpdateProductInStore(StoreProduct entity)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath);
            var products = lines.ToList();
            bool productFound = false;

            for (int i = 0; i < products.Count; i++)
            {
                var fields = lines[i].Split(',');

                if (fields[0] == entity.ProductName)
                {
                    productFound = true;

                    for (int storeIdx = 1; storeIdx < fields.Length; storeIdx += 3)
                    {
                        if (int.Parse(fields[storeIdx]) == entity.StoreId)
                        {
                            fields[storeIdx + 1] = entity.Amount.ToString();
                            fields[storeIdx + 1] = entity.Price.ToString();
                            products[i] = string.Join(",", fields);
                            System.IO.File.WriteAllLines(_filePath, lines);
                            return;
                        }
                    }
                    fields.Append(entity.StoreId.ToString());
                    fields.Append(entity.Amount.ToString());
                    fields.Append(entity.Price.ToString());
                    System.IO.File.WriteAllLines(_filePath, lines);
                    return;
                }
            }

            throw new Exception($"Product with name {entity.ProductName} doesn't exist");
        }

        public async Task<StoreProduct> GetProductInStoreAsync(int storeId, string productName)
        {
            var productInfo = await GetProductInAllStores(productName);
            if (productInfo != null)
            {
                return productInfo.Find(s => s.StoreId == storeId);
            }
            return null;
        }

        public async Task RemoveProductFromStoreAsync(StoreProduct entity)
        {
            var lines = (await System.IO.File.ReadAllLinesAsync(_filePath)).ToList();
            string pattern = $@"^{Regex.Escape(entity.ProductName)},";
            Regex regex = new Regex(pattern);
            var idx = lines.FindIndex(l => regex.IsMatch(l));
            if (idx == -1) return;

            var parts = lines[idx].Split(',');
            var newStores = new List<string>([parts[0]]);
            for (int i = 1; i < parts.Length; i += 3)
            {
                if (int.Parse(parts[i]) != entity.StoreId)
                {
                    newStores.Add(parts[i]);
                    newStores.Add(parts[i+1]);
                    newStores.Add(parts[i+2]);
                }
            }
            lines[idx] = string.Join(",", newStores);
            System.IO.File.WriteAllLines(_filePath, lines);
        }
    }
}
