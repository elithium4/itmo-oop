using Lab3.Repositories.Model;
using Lab3.Services;
using Lab3.Services.DTO;
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
        public async Task<ActionResult<int>> PurchaseFromStore(int storeId, List<ProductPurchaseDTO> purchase)
        {
            try
            {
                var total = await _storeProductService.BuyProductsFromStore(storeId, purchase);
                return Ok(total);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cheapestStore")]
        public async Task<ActionResult<StoreDTO>> GetCheapestStoreForProduct(string productName)
        {
            try
            {
                var store = await _storeProductService.FindCheapestStoreByProductName(productName);
                return Ok(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<List<ProductPurchaseDTO>>>> GetAllPossiblePurchases(int storeId, int money)
        {
            try
            {
                var res = await _storeProductService.FindAllProductsForMoneyAmount(storeId, money);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
