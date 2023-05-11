using Microsoft.AspNetCore.Mvc;
using NorthwindBL;
using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;

namespace NorthwindApi.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderServices _orderServices;
        public OrdersController(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll() 
        {
            IEnumerable<OrderDTO> orders = await _orderServices.GetAllOrdersAsync();

            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            OrderDTO? order = await _orderServices.GetOrderByIdAsync(id);

            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpGet("getByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetByCustomer(string customerId)
        {
            if (customerId == null || customerId.Length != 5) return BadRequest(CustomerErrorMessages.BadIdLength);

            IEnumerable<OrderDTO> orders = await _orderServices.GetOrdersByCustomerIdAsync(customerId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("getByCustomerAndEmployee/{customerId}/{employeeId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            if (customerId is null || customerId.Length != 5) return BadRequest(CustomerErrorMessages.BadIdLength);
            if (employeeId <= 0) return BadRequest(OrderErrorMessages.InvalidEmployeeId);

            IEnumerable<OrderDTO> orders = await _orderServices.GetOrdersByCustomerAndEmployeeAsync(customerId, employeeId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderCreateDTO>> Create(OrderCreateDTO newOrder)
        {
            if (!ModelState.IsValid) return BadRequest(OrderErrorMessages.ModelNotValid);

            OrderDTO? createdOrder = await _orderServices.AddOrderAsync(newOrder);

            if (createdOrder is null) return BadRequest(ProductErrorMessages.EntitiesNotFound);
            return CreatedAtAction(nameof(GetById), new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDTO orderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(OrderErrorMessages.ModelNotValid);

            bool result = await _orderServices.UpdateOrderAsync(orderDTO);

            if (result) return NoContent();
            return BadRequest(OrderErrorMessages.OrderOrEntitesNotFound);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _orderServices.DeleteOrderAsync(id);
            
            if (result) return NoContent();
            return BadRequest();
        }
    }
}
