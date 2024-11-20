using Lab3.Repositories.Model;
using Lab3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly StoreProductService _storeProductService;

        public PurchaseController(StoreProductService storeProductService)
        {
            _storeProductService = storeProductService;
        }

        [HttpPut("store")]
        public async Task<ActionResult<double>> PurchaseFromStore(int storeId, List<ProductPurchase> purchase)
        {
            var total = await _storeProductService.BuyProductsFromStore(storeId, purchase);
            return Ok(total);
        }

        [HttpGet("cheapestStore")]
        public async Task<ActionResult<Store>> GetCheapestStoreForProduct(string productName)
        {
            var store = await _storeProductService.FindCheapestStoreByProductName(productName);
            return Ok(store);
        }

    }
}
