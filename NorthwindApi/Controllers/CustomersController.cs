using Microsoft.AspNetCore.Mvc;
using NorthwindBL;
using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;

namespace NorthwindApi.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerServices _customerServices;
        public CustomersController(CustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAll()
        {
            IEnumerable<CustomerDTO> customers = await _customerServices.GetAllCustomersAsync();

            if (!customers.Any()) return NotFound(CustomerErrorMessages.NotFound);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> Get(string id)
        {
            if (id.Length != 5) return BadRequest(CustomerErrorMessages.BadIdLength);
            
            CustomerDTO? customerDTO = await _customerServices.GetByIdAsync(id);

            if (customerDTO == null) return NotFound(CustomerErrorMessages.NotFound);
            return Ok(customerDTO);
        }

        [HttpGet("CompanyName/{companyName}")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetByCompany(string companyName)
        {
            IEnumerable<CustomerDTO>? customerDTOs = await _customerServices.GetByCompanyNameAsync(companyName);

            if (customerDTOs is null) return NotFound(CustomerErrorMessages.NotFound);
            return Ok(customerDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCreateDTO>> AddCustomer(CustomerCreateDTO newCustomer)
        {
            if (!ModelState.IsValid || newCustomer.CustomerID.Length != 5) return BadRequest(CustomerErrorMessages.ModelNotValid);

            CustomerCreateDTO? addedCustomer = await _customerServices.AddCustomerAsync(newCustomer);

            if (addedCustomer is null) return BadRequest(CustomerErrorMessages.CustomerExists);
            else return CreatedAtAction(nameof(Get), new { id = addedCustomer.CustomerID }, addedCustomer);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCutomer(string id)
        {
            bool result = await _customerServices.DeleteCustomerAsync(id);
            
            if (result) return NoContent();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(CustomerErrorMessages.ModelNotValid);

            bool result = await _customerServices.UpdateCustomerAsync(customerDTO);

            if(result) return NoContent();
            return NotFound(CustomerErrorMessages.NotFound);
        }
    }
}
