using AutoMapper;
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

        public OrderDTO AddOrder(OrderCreateDTO order)
        {
            Order newOrder = _mapper.Map<Order>(order);

            newOrder.OrderDate = DateTime.Now;

            _orderRepository.AddOrder(newOrder);

            return _mapper.Map<OrderDTO>(newOrder);
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool DeleteOrder(int id)
        {
            Order? order = _orderRepository.GetOrderById(id);

            if (order is null) return false; 

            _orderRepository.DeleteOrder(order);

            return true;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            IEnumerable<Order> orders = _orderRepository.GetAllOrders();
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public OrderDTO? GetOrderById(int id)
        {
            Order? order = _orderRepository.GetOrderById(id);
            return _mapper.Map<OrderDTO?>(order);
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerAndEmployee(string customerId, int employeeId)
        {
            IEnumerable<Order> orders = _orderRepository.GetOrdersByCustomerAndEmployee(customerId, employeeId);
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerId(string id)
        {
            IEnumerable<Order> orders = _orderRepository.GetOrdersByCustomerId(id);
            return orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

    }
}
