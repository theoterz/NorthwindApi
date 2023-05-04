using Microsoft.EntityFrameworkCore;
using NorthwindDAL.Interfaces;
using NorthwindModels.Models;

namespace NorthwindDAL.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _appDbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _appDbContext.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrdersByCustomerAndEmployeeAsync(string customerId, int employeeId)
        {
            return await _appDbContext.Orders.Where(o => o.CustomerID!.Equals(customerId) && o.EmployeeID.Equals(employeeId)).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsync(string id)
        {
            return await _appDbContext.Orders.Where(o => o.CustomerID!.Equals(id)).ToListAsync();
        }

        public bool OrderExists(int id)
        {
            return _appDbContext.Orders.Any(o => o.OrderID == id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
