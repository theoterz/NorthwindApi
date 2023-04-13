using Microsoft.AspNetCore.Mvc;
using NorthwindBL;
using NorthwindModels.DTOs;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productServices;
        public ProductController(ProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAll()
        {
            IEnumerable<ProductDTO> products = _productServices.GetAllProducts();

            if (!products.Any()) return NotFound();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDTO> GetById(int id) 
        {
            ProductDTO? product = _productServices.GetByProductId(id);

            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct(ProductCreateDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            ProductDTO? createdProduct = _productServices.AddProduct(productDTO);

            if (createdProduct is null) return BadRequest("Product id already exists.");
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut]
        public IActionResult Update(ProductUpdateDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool result = _productServices.UpdateProduct(product);

            if (result) return NoContent();
            return NotFound(); 
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool result = _productServices.DeleteProduct(id);

            if (result) return NoContent();
            return BadRequest();
        }
    }
}
