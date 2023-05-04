using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL
{
    public class OrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderServices(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO?> AddOrderAsync(OrderCreateDTO order)
        {
            try
            {
                Order newOrder = _mapper.Map<Order>(order);

                newOrder.OrderDate = DateTime.Now;

                await _orderRepository.AddOrderAsync(newOrder);

                return _mapper.Map<OrderDTO>(newOrder);
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public async Task<bool> DeleteOrderAsync(int id)
        {
            Order? order = await _orderRepository.GetOrderByIdAsync(id);

            if (order is null) return false; 

            await _orderRepository.DeleteOrderAsync(order);

            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            Order? order = await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDTO?>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerAndEmployeeAsync(string customerId, int employeeId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByCustomerAndEmployeeAsync(customerId, employeeId);
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(string id)
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByCustomerIdAsync(id);
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<bool> UpdateOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                if (!_orderRepository.OrderExists(orderDTO.OrderID)) return false;

                Order order = _mapper.Map<Order>(orderDTO);

                await _orderRepository.UpdateOrderAsync(order);

                return true;
            }
            catch(DbUpdateException)
            {
                return false;
            }
        }
    }
}
