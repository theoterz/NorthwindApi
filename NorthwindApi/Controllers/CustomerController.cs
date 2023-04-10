using Microsoft.AspNetCore.Mvc;
using NorthwindDAL.Repositories;
using NorthwindModels.DTOs;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
        {
            IEnumerable<CustomerDTO> customers = _customerRepository.GetAll();

            if (!customers.Any()) return NotFound();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> Get(string id)
        {
            if (id.Length != 5) return BadRequest("The id lenght must be exactly 5 characters");
            
            CustomerDTO? customerDTO = _customerRepository.GetById(id);

            if (customerDTO == null) return NotFound();
            return Ok(customerDTO);
        }

        [HttpGet("getByCompany/{companyName}")]
        public ActionResult<IEnumerable<CustomerDTO>> GetByCompany(string companyName)
        {
            IEnumerable<CustomerDTO> customerDTOs = _customerRepository.GetByCompanyName(companyName);

            if (!customerDTOs.Any()) return NotFound();
            return Ok(customerDTOs);

        }

        [HttpPost]
        public ActionResult<CustomerCreateDTO> AddCustomer([FromBody] CustomerCreateDTO newCustomer)
        {
            if (!ModelState.IsValid || newCustomer.CustomerID.Length != 5) return BadRequest();

            CustomerCreateDTO? addedCustomer = _customerRepository.AddCustomer(newCustomer);

            if (addedCustomer is null) return BadRequest();
            else return CreatedAtAction(nameof(Get), new { id = addedCustomer.CustomerID }, addedCustomer);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCutomer(string id)
        {
            bool result = _customerRepository.DeleteCustomer(id);
            
            if (result) return NoContent();
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateCustomer(CustomerUpdateDTO customerDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            bool result = _customerRepository.UpdateCustomer(customerDTO);

            if(result) return NoContent();
            return NotFound();
        }

    }
}
