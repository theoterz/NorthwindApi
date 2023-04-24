using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                ProductDTO? createdProduct = _productServices.AddProduct(productDTO);

                if (createdProduct is null) return BadRequest("Product id already exists.");
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (DbUpdateException ex)
            {

                string? errorMessage = string.Empty;
                var innerException = ex.InnerException as SqlException;
                /*The inner exception number reffers to an error taht occurs during an insert command and the value of the foreign key doesn't exist in the table
                the foreing key refers to.*/
                if (innerException is not null && innerException.Number == 547)
                {
                    if (innerException.Message.Contains("Supplier")) errorMessage = "Supplier doesn't exist\n";
                    else if (innerException.Message.Contains("Category")) errorMessage = "Category doesn't exist\n";

                    return BadRequest(errorMessage);
                }
                else return BadRequest("An error during the update of the database occured!");
            }
        }


        [HttpPut]
        public IActionResult Update(ProductDTO product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                bool result = _productServices.UpdateProduct(product);

                if (result) return NoContent();
                return NotFound();
            }
            catch (DbUpdateException ex)
            {

                string? errorMessage = string.Empty;
                var innerException = ex.InnerException as SqlException;
                /*The inner exception number reffers to an error taht occurs during an insert command and the value of the foreign key doesn't exist in the table
                the foreing key refers to.*/
                if (innerException is not null && innerException.Number == 547)
                {
                    if (innerException.Message.Contains("Supplier")) errorMessage = "Supplier doesn't exist\n";
                    else if (innerException.Message.Contains("Category")) errorMessage = "Category doesn't exist\n";

                    return BadRequest(errorMessage);
                }
                else return BadRequest("An error during the update of the database occured!");
            }
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
