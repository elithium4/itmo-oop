using Lab3.Model;
using Lab3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController: ControllerBase
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("stores")]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }

        [HttpPost("createStore")]
        public async Task<ActionResult> CreateStore(Store store)
        {
            await _storeService.CreateStore(store);
            return Ok();
        }

        //[HttpPut("productAmount")]
        //public async Task<ActionResult> SetProductAmount(Product product, int amount)
        //{

        //}



    }
}
