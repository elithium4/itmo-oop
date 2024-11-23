using Lab3.Repositories.Model;
using Lab3.Services;
using Lab3.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;


        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получить список всех товаров.
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            var stores = await _productService.GetAllProducts();
            return Ok(stores);
        }

        /// <summary>
        /// Создать товар
        /// </summary>
        /// <param name="name">Название товара</param>
        [HttpPost("create")]
        public async Task<ActionResult> CreateProduct(string name)
        {
            try
            {
                await _productService.CreateProduct(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}