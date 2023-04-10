using Microsoft.AspNetCore.Mvc;
using NorthwindDAL.Repositories;
using NorthwindModels.DTOs;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetAll() 
        {
            IEnumerable<OrderDTO> orders = _orderRepository.GetAllOrders();

            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public ActionResult<OrderDTO> GetById(int id)
        {
            OrderDTO? order = _orderRepository.GetOrderById(id);

            if (order is null) return NotFound();
            return Ok(order);
        }

        [HttpGet("getByCustomer/{customerId}")]
        public ActionResult<IEnumerable<OrderDTO>> GetByCustomer(string customerId)
        {
            if (customerId == null || customerId.Length != 5) return BadRequest();

            IEnumerable<OrderDTO> orders = _orderRepository.GetOrdersByCustomerId(customerId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpGet("getByCustomerAndEmployee/{customerId}/{employeeId:int}")]
        public ActionResult<IEnumerable<OrderDTO>> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            if (customerId is null || customerId.Length != 5 || employeeId <= 0) return BadRequest();

            IEnumerable<OrderDTO> orders = _orderRepository.GetOrdersByCustomerAndEmployee(customerId, employeeId);
            
            if (!orders.Any()) return NotFound();
            return Ok(orders);
        }

        [HttpPost]
        public ActionResult<OrderCreateDTO> Create(OrderCreateDTO newOrder)
        {
            if (!ModelState.IsValid) return BadRequest();

            OrderDTO createdOrder = _orderRepository.AddOrder(newOrder);

            return CreatedAtAction(nameof(GetById), new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool result = _orderRepository.DeleteOrder(id);
            
            if (result) return NoContent();
            return BadRequest();
        }
    }
}
