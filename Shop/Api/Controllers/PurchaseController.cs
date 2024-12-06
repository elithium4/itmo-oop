using BLL.Services;
using BLL.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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

        /// <summary>
        /// Купить товар в магазине
        /// </summary>
        /// <param name="storeId">Id магазина</param>
        /// /// <param name="purchase">Данные о покупке товара</param>
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

        /// <summary>
        /// Найти магазин, в котором товар наиболее дешевый
        /// </summary>
        /// /// <param name="productName">Название товара</param>
        [HttpGet("cheapestStore")]
        public async Task<ActionResult<StorePurchaseDTO>> GetCheapestStoreForProduct(string productName)
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

        /// <summary>
        /// Найти магазин, в котором покупка списка товаров стоит меньше всего
        /// </summary>
        /// <param name="list">Список товаров для покупки</param>
        [HttpPost("cheapestListStore")]
        public async Task<ActionResult<List<StorePurchaseDTO>>> GetCheapestListStore(List<ProductPurchaseDTO> list)
        {
            try
            {
                var res = await _storeProductService.FindCheapestStoreForList(list);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Найти все возможные покупки в магазине на заданную сумму (часть суммы может остаться)
        /// </summary>
        /// <param name="storeId">Id магазина</param>
        /// /// <param name="money">Сумма денег</param>
        [HttpGet("allPurchases")]
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
