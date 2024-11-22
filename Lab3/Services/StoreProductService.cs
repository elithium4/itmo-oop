using Lab3.Repositories;
using Lab3.Repositories.Model;
using Lab3.Services.Exceptions;
using Lab3.Services.DTO;
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
                throw new ProductDoesNotExistsException(name);
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

        public async Task<double> BuyProductsFromStore(int storeId, List<ProductPurchaseDTO> products)
        {
            var productsInStore = await _productRepository.GetProductsByStoreIdAsync(storeId);
            double total = 0;
            foreach (var item in products)
            {
                var storeProductInfo = productsInStore.Find(p => p.ProductName == item.ProductName);
                if (storeProductInfo == null)
                {
                    throw new NoStoreProductException(storeId, item.ProductName);
                }
                if (item.Amount <= 0)
                {
                    throw new NonPositiveAmountException();
                }
                if (storeProductInfo.Amount < item.Amount)
                {
                    throw new NotEnoughProduException(storeId, item.ProductName);
                }
                total += item.Amount * storeProductInfo.Price;
                storeProductInfo.Amount -= item.Amount;

            }
            foreach (var item in productsInStore)
            {
                if (item.Amount <= 0)
                {
                    await _productRepository.RemoveProductFromStoreAsync(item);

                } else
                {
                    await _productRepository.AddOrUpdateProductInStore(item);
                }
            }
            return total;
        }

        public async Task AddProductToStore(ProductStoreDetail productDetail)
        {
            var detail = await _productRepository.GetProductInStoreAsync(productDetail.StoreId, productDetail.ProductName);
            if (detail != null)
            {
                throw new ProductAlreadyExistsInSToreException(productDetail.StoreId, productDetail.ProductName);
            }
            if (productDetail.Amount <= 0)
            {
                throw new NonPositiveAmountException();
            }
            if (productDetail.Price <= 0)
            {
                throw new NonPositivePriceException();
            }
            await _productRepository.AddOrUpdateProductInStore(productDetail);
        }

        public async Task UpdateProductStorePrice(int storeId, string productName, int price)
        {
            var productDetail = await _productRepository.GetProductInStoreAsync(storeId, productName);
            if (productDetail == null)
            {
                throw new NoStoreProductException(storeId, productName);
            }

            if (price <= 0)
            {
                throw new NonPositivePriceException();
            }
            productDetail.Price = price;
            await _productRepository.AddOrUpdateProductInStore(productDetail);
        }

        public async Task UpdateProductStoreAmount(int storeId, string productName, int amount)
        {
            var productDetail = await _productRepository.GetProductInStoreAsync(storeId, productName);
            if (productDetail == null)
            {
                throw new NoStoreProductException(storeId, productName);
            }

            if (amount <= 0)
            {
                throw new NonPositiveAmountException();
            }
            productDetail.Amount = amount;
            await _productRepository.AddOrUpdateProductInStore(productDetail);
        }

        public async Task<List<List<ProductPurchaseDTO>>> FindAllProductsForMoneyAmount(int storeId, int money)
        {
            var combos = new List<List<ProductPurchaseDTO>> { };
            var storeProducts = await _productRepository.GetProductsByStoreIdAsync(storeId);
            if (storeProducts == null)
            {
                throw new NoProductsInStoreException(storeId);
            }
            FindProductCombo(storeProducts, money, [], combos);
            return combos;
        }

        private void FindProductCombo(List<ProductStoreDetail> productsInStore, int remainingMoney, List<ProductPurchaseDTO> currentCombo,  List<List<ProductPurchaseDTO>> combos, int searchStart = 0)
        {

            if (remainingMoney <=0)
            {
                combos.Add(currentCombo);
                return;
            }

            for (int itemIdx = 0; itemIdx < productsInStore.Count; itemIdx ++)
            {
                var item = productsInStore[itemIdx];
                for (int itemCount = 1; itemCount < item.Amount; itemCount++)
                {
                    int totalPrice = item.Price * itemCount;
                    if (totalPrice > remainingMoney)
                    {
                        break;
                    }

                    var productInCombo = currentCombo.Find(p => p.ProductName == item.ProductName);
                    if (productInCombo == null)
                    {
                        currentCombo.Add(new ProductPurchaseDTO { Amount = itemCount, ProductName = item.ProductName  });
                    } else
                    {
                        productInCombo.Amount += itemCount;
                    }

                    FindProductCombo(productsInStore, remainingMoney - totalPrice, currentCombo, combos, itemIdx);
                }
            }
        }

    }
}
