using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders();
        public Order? GetOrderById(int id);
        public IEnumerable<Order> GetOrdersByCustomerId(string id);
        public IEnumerable<Order> GetOrdersByCustomerAndEmployee(string customerId, int employeeId);
        public void AddOrder(Order order);
        public void DeleteOrder(Order order);
    }
}
