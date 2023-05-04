﻿using Microsoft.AspNetCore.Mvc;
using NorthwindBL;
using NorthwindModels.DTOs;

namespace NorthwindApi.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServices _productServices;
        public ProductsController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            IEnumerable<ProductDTO> products = await _productServices.GetAllProductsAsync();

            if (!products.Any()) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id) 
        {
            ProductDTO? product = await _productServices.GetByProductIdAsync(id);

            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductCreateDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            ProductDTO? createdProduct = await _productServices.AddProductAsync(productDTO);

            if (createdProduct is null) return BadRequest("The supplier or the category doesn't exist");
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool result = await _productServices.UpdateProductAsync(product);

            if (result) return NoContent();
            return BadRequest("The product, the supplier or the category doesn't exist");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _productServices.DeleteProductAsync(id);

            if (result) return NoContent();
            return BadRequest();
        }
    }
}