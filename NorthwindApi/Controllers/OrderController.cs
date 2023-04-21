using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NorthwindBL;
using NorthwindModels.DTOs;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _orderServices;
        public OrderController(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetAll() 
        {
            IEnumerable<OrderDTO> orders = _orderServices.GetAllOrders();

            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public ActionResult<OrderDTO> GetById(int id)
        {
            OrderDTO? order = _orderServices.GetOrderById(id);

            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpGet("getByCustomer/{customerId}")]
        public ActionResult<IEnumerable<OrderDTO>> GetByCustomer(string customerId)
        {
            if (customerId == null || customerId.Length != 5) return BadRequest();

            IEnumerable<OrderDTO> orders = _orderServices.GetOrdersByCustomerId(customerId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("getByCustomerAndEmployee/{customerId}/{employeeId:int}")]
        public ActionResult<IEnumerable<OrderDTO>> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            if (customerId is null || customerId.Length != 5 || employeeId <= 0) return BadRequest();

            IEnumerable<OrderDTO> orders = _orderServices.GetOrdersByCustomerAndEmployee(customerId, employeeId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpPost]
        public ActionResult<OrderCreateDTO> Create(OrderCreateDTO newOrder)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                OrderDTO? createdOrder = _orderServices.AddOrder(newOrder);

                if (createdOrder is null) return BadRequest();
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.OrderID }, createdOrder);

            }catch (DbUpdateException ex)
            {
                string? errorMessage = string.Empty;
                var innerException = ex.InnerException as SqlException;
                /*The inner exception number reffers to an error taht occurs during an insert command and the value of the foreign key doesn't exist in the table
                the foreing key refers to.*/
                if (innerException is not null && innerException.Number == 547)
                {
                    if (innerException.Message.Contains("Customer")) errorMessage = "Customer doesn't exist\n";
                    else if (innerException.Message.Contains("Employee")) errorMessage = "Employee doesn't exist\n";
                    else if (innerException.Message.Contains("Ship")) errorMessage = "Shipper doesn't exist\n";

                    return BadRequest(errorMessage);
                }
                else return BadRequest("An error during the update of the database occured!");
            }
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool result = _orderServices.DeleteOrder(id);
            
            if (result) return NoContent();
            return BadRequest();
        }
    }
}
