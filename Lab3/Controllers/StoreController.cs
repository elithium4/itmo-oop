using Lab3.Repositories.Model;
using Lab3.Services;
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
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStore(Store store)
        {
            await _storeService.CreateStore(store);
            return Ok();
        }

        [HttpPut("addProducts")]
        public async Task<ActionResult> AddProductToStore(ProductStoreDetail product)
        {
            await _storeProductService.AddProductToStore(product);
            return Ok();
        }

        //[HttpPut("productAmount")]
        //public async Task<ActionResult> SetProductAmount(Product product, int amount)
        //{

        //}



    }
}
