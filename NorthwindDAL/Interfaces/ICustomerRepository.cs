using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> GetAll();
        public Customer? GetById(string id);
        public IEnumerable<Customer> GetByCompanyName(string name);
        public void AddCustomer(Customer customer);
        public void DeleteCustomer(Customer customer);
        public void UpdateCustomer(Customer customer);
        public bool CustomerExists(string id);
    }
}
