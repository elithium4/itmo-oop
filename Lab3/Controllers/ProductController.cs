﻿using Lab3.Repositories.Model;
using Lab3.Services;
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

        [HttpGet("all")]
        public async Task<ActionResult<List<Store>>> GetAllProducts()
        {
            var stores = await _productService.GetAllProducts();
            return Ok(stores);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStore(string name)
        {
            var result = await _productService.CreateProduct(name);
            return result ? Ok() : Conflict("Product with this name already exists");
        }

        //[HttpPut("productAmount")]
        //public async Task<ActionResult> SetProductAmount(Product product, int amount)
        //{

        //}



    }
}