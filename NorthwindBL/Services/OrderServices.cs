using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindBL.Interfaces;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IEntityRepository<Order, int> _orderRepository;
        private readonly IMapper _mapper;
        public OrderServices(IEntityRepository<Order, int> orderRepository, IMapper mapper)
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

                await _orderRepository.CreateEntityAsync(newOrder);

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
            Order? order = await _orderRepository.GetEntityByIdAsync(id);

            if (order is null) return false;

            await _orderRepository.DeleteEntityAsync(order);

            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllEntitiesAsync();
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            Order? order = await _orderRepository.GetEntityByIdAsync(id);
            return _mapper.Map<OrderDTO?>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerAndEmployeeAsync(string customerId, int employeeId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllEntitiesAsync(o => o.CustomerID!.Equals(customerId) && o.EmployeeID.Equals(employeeId));
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(string id)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllEntitiesAsync(o => o.CustomerID!.Equals(id));
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public async Task<bool> UpdateOrderAsync(OrderDTO orderDTO)
        {
            try
            {
                if (!_orderRepository.EntityExists(orderDTO.OrderID)) return false;

                Order order = _mapper.Map<Order>(orderDTO);

                await _orderRepository.UpdateEntityAsync(order);

                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
