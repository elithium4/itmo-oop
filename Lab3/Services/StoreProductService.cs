using Lab3.Repositories;
using Lab3.Repositories.Model;
using System.Xml.Linq;

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

        public async Task<List<Store>> FindCheapestStoreByProductName(string name)
        {
            var productInStores = await _productRepository.GetProductInAllStores(name);
            if (productInStores == null)
            {
                throw new Exception($"No product with name {name}");
            }
            double? minimumPrice = null;
            List<Store> suitableStores = new List<Store>();
            foreach (var productInStore in productInStores)
            {
                if (minimumPrice == null)
                {
                    minimumPrice = productInStore.Price;
                    suitableStores.Add(await _storeRepository.GetStoreByIdAsync(productInStore.StoreId));
                    continue;
                } 

                if (productInStore.Price == minimumPrice)
                {
                    suitableStores.Add(await _storeRepository.GetStoreByIdAsync(productInStore.StoreId));
                }

                if (productInStore.Price < minimumPrice)
                {
                    minimumPrice = productInStore.Price;
                    suitableStores = [ await _storeRepository.GetStoreByIdAsync(productInStore.StoreId) ];
                }
            }
            return suitableStores;

        }

        //public async Task<List<List<Product>>> getShoppingListByMoneyAmount(int storeId, int moneyAmount)
        //{
        //    var availableProduct = _productRepository.GetProductsByStoreIdAsync(storeId);

        //}

        //private void FindProductsSet(List<ProductStoreDetail> products, int remainingBudget, List<ProductStoreDetail> currentCombination, List<List<Product>> combinations)
        //{
        //    if (remainingBudget < 0) return; // Выходим, если превышен бюджет
        //    if (remainingBudget == 0) // Если остался ровно 0, добавляем комбинацию в результаты
        //    {
        //        combinations.Add(new List<ProductStoreDetail>(currentCombination));
        //        return;
        //    }

        //    for (int i = 0; i < products.Count; i++)
        //    {
        //        // Добавляем текущий товар в текущую комбинацию
        //        currentCombination.Add(products[i]);
        //        // Рекурсивно ищем комбинации для оставшегося бюджета
        //        FindProductsSet(products, remainingBudget - products[i].Price, currentCombination, combinations);
        //        // Убираем товар, чтобы исследовать другие комбинации
        //        currentCombination.RemoveAt(currentCombination.Count - 1);
        //    }
        //}

        public async Task<double> BuyProductsFromStore(int storeId, List<ProductPurchase> products)
        {
            var productsInStore = await _productRepository.GetProductsByStoreIdAsync(storeId);
            double total = 0;
            foreach (var item in products)
            {
                var storeProductInfo = productsInStore.Find(p => p.ProductName == item.ProductName);
                if (storeProductInfo == null || storeProductInfo.Amount < item.Amount)
                {
                    throw new Exception("Can't purchase: no required product");
                }
                total += item.Amount * storeProductInfo.Price;
                await _productRepository.UpdateProductInStore(new ProductStoreDetail
                {
                    ProductName = item.ProductName,
                    StoreId = storeId,
                    Amount = storeProductInfo.Amount - item.Amount,
                    Price = storeProductInfo.Price
                });

            }
            return total;
        }

        public async Task AddProductToStore(ProductStoreDetail productDetail)
        {
            await _productRepository.UpdateProductInStore(productDetail);
        }

        public async Task UpdateProductStorePrice(int storeId, string productName, double price)
        {

        }

        //public async Task<List<Store>> FindBestDealForProductSet(List<ProductPurchase> products)
        //{

        //}
    }
}
