using NorthwindModels.DTOs;

namespace NorthwindDAL.Repositories
{
    public interface IOrderRepository
    {
        public IEnumerable<OrderDTO> GetAllOrders();
        public OrderDTO? GetOrderById(int id);
        public IEnumerable<OrderDTO> GetOrdersByCustomerId(string id);
        public IEnumerable<OrderDTO> GetOrdersByCustomerAndEmployee(string customerId, int employeeId);
        public OrderDTO AddOrder(OrderCreateDTO order);
        public bool DeleteOrder(int id);
    }
}
