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

        public void AddOrder(Order order)
        {
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _appDbContext.Orders.Remove(order);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _appDbContext.Orders;
        }

        public Order? GetOrderById(int id)
        {
            return _appDbContext.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrdersByCustomerAndEmployee(string customerId, int employeeId)
        {
            return _appDbContext.Orders.Where(o => o.CustomerID!.Equals(customerId) && o.EmployeeID.Equals(employeeId));
        }

        public IEnumerable<Order> GetOrdersByCustomerId(string id)
        {
            return _appDbContext.Orders.Where(o => o.CustomerID!.Equals(id));
        }
    }
}
