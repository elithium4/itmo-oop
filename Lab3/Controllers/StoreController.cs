using Lab3.Services;
using Lab3.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController: ControllerBase
    {
        private readonly StoreService _storeService;
        private readonly StoreProductService _storeProductService;

        public StoreController(StoreService storeService, StoreProductService storeProductService)
        {
            _storeService = storeService;
            _storeProductService = storeProductService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<StoreDTO>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }

        [HttpGet("one")]
        public async Task<ActionResult<FullStoreInfoDTO>> GetStoresById(int storeId)
        {
            try
            {
                var stores = await _storeService.GetStoreById(storeId);
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStore(StoreDTO store)
        {
            await _storeService.CreateStore(store);
            return Ok();
        }

        [HttpPut("addProducts")]
        public async Task<ActionResult> AddProductToStore(StoreProductDTO product)
        {
            try
            {
                await _storeProductService.AddProductToStore(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("price")]
        public async Task<ActionResult> SetProductPricetInStore(int storeId, string productName, int price)
        {
            try
            {
                await _storeProductService.UpdateProductStorePrice(storeId, productName, price);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("amount")]
        public async Task<ActionResult> SetProductAmounttInStore(int storeId, string productName, int amount)
        {
            try
            {
                await _storeProductService.UpdateProductStoreAmount(storeId, productName, amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
