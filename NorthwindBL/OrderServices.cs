using AutoMapper;
using NorthwindDAL.Repositories;
using NorthwindModels.DTOs;
using NorthwindModels.Models;


namespace NorthwindDAL.Services
{
    public class OrderServices : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public OrderServices(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public OrderDTO AddOrder(OrderCreateDTO order)
        {
            Order newOrder = _mapper.Map<Order>(order);

            newOrder.OrderDate = DateTime.Now;

            _appDbContext.Orders.Add(newOrder);
            _appDbContext.SaveChanges();

            return _mapper.Map<OrderDTO>(newOrder);
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool DeleteOrder(int id)
        {
            Order? order = _appDbContext.Orders.Find(id);

            if (order is null) return false; 

            _appDbContext.Orders.Remove(_mapper.Map<Order>(order));
            _appDbContext.SaveChanges();

            return true;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return _appDbContext.Orders.Select(o => _mapper.Map<OrderDTO>(o));
        }

        public OrderDTO? GetOrderById(int id)
        {
            return _mapper.Map<OrderDTO>(_appDbContext.Orders.Find(id));
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerAndEmployee(string customerId, int employeeId)
        {
            return _appDbContext.Orders.Where(o => o.CustomerID!.Equals(customerId) && o.EmployeeID.Equals(employeeId)).Select(o => _mapper.Map<OrderDTO>(o));
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerId(string id)
        {
            return _appDbContext.Orders.Where(o => o.CustomerID!.Equals(id)).Select(o => _mapper.Map<OrderDTO>(o));
        }

    }
}
