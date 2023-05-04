using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllOrdersAsync();
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<List<Order>> GetOrdersByCustomerIdAsync(string id);
        public Task<List<Order>> GetOrdersByCustomerAndEmployeeAsync(string customerId, int employeeId);
        public Task AddOrderAsync(Order order);
        public Task DeleteOrderAsync(Order order);
        public Task UpdateOrderAsync(Order order);
        public bool OrderExists(int id);
    }
}
