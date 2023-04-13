using NorthwindDAL.Interfaces;
using NorthwindModels.Models;

namespace NorthwindDAL.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddCustomer(Customer customer)
        {
            _appDbContext.Customers.Add(customer);
            _appDbContext.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _appDbContext.Customers.Remove(customer);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _appDbContext.Customers;
        }

        public IEnumerable<Customer> GetByCompanyName(string name)
        {
            return _appDbContext.Customers.Where(c => c.CompanyName.Contains(name));
        }

        public Customer? GetById(string id)
        {
            return _appDbContext.Customers.Find(id);
        }

        public void UpdateCustomer(Customer customer)
        {
            _appDbContext.Customers.Update(customer);
            _appDbContext.SaveChanges();
        }

        public bool CustomerExists(string id)
        {
            return _appDbContext.Customers.Any(c => c.CustomerID.Equals(id));
        }
    }
}
