using DAL.Repositories;
using DAL.Repositories.Model;
using BLL.Services.Exceptions;
using BLL.Services.DTO;
using System.Xml.Linq;
using AutoMapper;

namespace BLL.Services
{
    public class StoreProductService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public StoreProductService(IStoreRepository storeRepository, IProductRepository productRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<StorePurchaseDTO>> FindCheapestStoreByProductName(string name)
        {
            var productInStores = await _productRepository.GetProductInAllStores(name);
            if (productInStores == null)
            {
                throw new ProductDoesNotExistsException(name);
            }
            int minimumPrice = int.MaxValue;
            List<Store> suitableStores = new List<Store>();
            foreach (var productInStore in productInStores)
            {

                if (productInStore.Price < minimumPrice)
                {
                    minimumPrice = productInStore.Price;
                    suitableStores = [await _storeRepository.GetStoreByIdAsync(productInStore.StoreId)];
                } else if (productInStore.Price == minimumPrice)
                {
                    suitableStores.Add(await _storeRepository.GetStoreByIdAsync(productInStore.StoreId));
                }
            }
            if (suitableStores.Count == 0)
            {
                throw new NoSuitableStoresException();
            }
            List<StorePurchaseDTO> storesForPurchase = new List<StorePurchaseDTO>();
            foreach (var item in suitableStores)
            {
                storesForPurchase.Add(new StorePurchaseDTO
                {
                    Store = _mapper.Map<StoreDTO>(item),
                    Cost = minimumPrice
                });
            }
            return storesForPurchase;

        }

        public async Task<int> BuyProductsFromStore(int storeId, List<ProductPurchaseDTO> products)
        {
            var productsInStore = await _productRepository.GetProductsByStoreIdAsync(storeId);
            int total = 0;
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

        public async Task AddProductToStore(StoreProductDTO productDetail)
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
            var store = await _storeRepository.GetStoreByIdAsync(productDetail.StoreId);
            if (store == null)
            {
                throw new StoreDoesNotExistException(productDetail.StoreId);
            }
            var product = await _productRepository.GetProductByNameAsync(productDetail.ProductName);
            if (product == null)
            {
                throw new ProductDoesNotExistsException(productDetail.ProductName);
            }
            await _productRepository.AddOrUpdateProductInStore(new StoreProduct
            {
                ProductName = productDetail.ProductName,
                StoreId = productDetail.StoreId,
                Amount = productDetail.Amount,
                Price = productDetail.Price,
                Store = store,
                Product = product
            });
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
            FindProductCombo(storeProducts, money, new List<ProductPurchaseDTO>(), combos);
            return combos;
        }

        private void FindProductCombo(List<StoreProduct> productsInStore, int remainingMoney, List<ProductPurchaseDTO> currentCombo,  List<List<ProductPurchaseDTO>> combos, int searchStart = 0)
        {
            if (searchStart >= productsInStore.Count && remainingMoney >= 0 && currentCombo.Count > 0)
            {
                combos.Add(new List<ProductPurchaseDTO>(currentCombo));
                return;
            }
            if (remainingMoney <=0 && currentCombo.Count > 0)
            {
                combos.Add(new List<ProductPurchaseDTO>(currentCombo));
                return;
            }

            for (int itemIdx = searchStart; itemIdx < productsInStore.Count; itemIdx ++)
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
                        productInCombo.Amount = itemCount;
                    }

                    FindProductCombo(productsInStore, remainingMoney - totalPrice, currentCombo, combos, itemIdx+1);
                    currentCombo.RemoveAt(currentCombo.Count - 1);
                }
            }
        }

        public async Task<StorePurchaseDTO> FindCheapestStoreForList(List<ProductPurchaseDTO> list)
        {
            Store cheapestStore = null;
            int lowestCost = int.MaxValue;
            var stores = await _storeRepository.GetAllStoresAsync();
            foreach (var store in stores)
            {
                var storeProducts = await _productRepository.GetProductsByStoreIdAsync(store.Id);
                int totalCost = 0;
                bool hasAllProducts = true;
                foreach (var product in list)
                {
                    var storeProduct = storeProducts.FirstOrDefault(p => p.ProductName == product.ProductName);
                    if (storeProduct == null || storeProduct.Amount < product.Amount)
                    {
                        hasAllProducts = false;
                        break;
                    }
                    totalCost += product.Amount * storeProduct.Price;
                }

                if (hasAllProducts && totalCost < lowestCost)
                {
                    cheapestStore = store;
                    lowestCost = totalCost;
                }
            }
            if (cheapestStore != null)
            {
                return new StorePurchaseDTO
                {
                    Store = _mapper.Map<StoreDTO>(cheapestStore),
                    Cost = lowestCost
                };
            }
            throw new NoSuitableStoresException();
        }

        public async Task<List<ProductInStoreDTO>> GetAllStoreProducts(int storeId)
        {
            var products = await _productRepository.GetProductsByStoreIdAsync(storeId);
            if (products == null)
            {
                throw new NoProductsInStoreException(storeId);
            }
            return _mapper.Map<List<ProductInStoreDTO>>(products);
        }

    }
}
