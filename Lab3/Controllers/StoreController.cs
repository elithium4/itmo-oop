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

        /// <summary>
        /// Получить список всех магазинов
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<StoreDTO>>> GetAllStores()
        {
            var stores = await _storeService.GetAllStores();
            return Ok(stores);
        }

        /// <summary>
        /// Получить информацию о конкретном магазине
        /// </summary>
        /// <param name="id">Id магазина</param>
        [HttpGet("one")]
        public async Task<ActionResult<StoreDTO>> GetStoresById(int id)
        {
            try
            {
                var stores = await _storeService.GetStoreById(id);
                return Ok(stores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создать новый магазин
        /// </summary>
        /// <param name="store">Параметры магазина</param>
        [HttpPost("create")]
        public async Task<ActionResult> CreateStore(CreateStoreDTO store)
        {
            await _storeService.CreateStore(store);
            return Ok();
        }

        /// <summary>
        /// Добавить товар в магазин
        /// </summary>
        /// <param name="product">Данные о добавляемом товаре</param>
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

        /// <summary>
        /// Задать цену товара в магазине
        /// </summary>
        /// <param name="storeId">Id магазина</param>
        /// /// <param name="productName">Название товара</param>
        /// /// <param name="price">Новая стоимость товара</param>
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

        /// <summary>
        /// Задать количество товара в магазине
        /// </summary>
        /// <param name="storeId">Id магазина</param>
        /// /// <param name="productName">Название товара</param>
        /// /// <param name="amount">Новое количество товара</param>
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

        /// <summary>
        /// Получить список товара в магазине
        /// </summary>
        /// <param name="storeId">Id магазина</param>
        [HttpGet("products")]
        public async Task<ActionResult<List<ProductInStoreDTO>>> GetStoreProductsById(int storeId)
        {
            try
            {
                var products = await _storeProductService.GetAllStoreProducts(storeId);
                return Ok(products);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
