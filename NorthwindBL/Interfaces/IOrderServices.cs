using NorthwindModels.DTOs;

namespace NorthwindBL.Interfaces
{
    public interface IOrderServices
    {
        Task<OrderDTO?> AddOrderAsync(OrderCreateDTO order);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerAndEmployeeAsync(string customerId, int employeeId);
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(string id);
        Task<bool> UpdateOrderAsync(OrderDTO orderDTO);
    }
}