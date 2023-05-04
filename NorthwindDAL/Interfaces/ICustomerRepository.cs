using NorthwindModels.Models;

namespace NorthwindDAL.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer?> GetByIdAsync(string id);
        public Task<List<Customer>?> GetByCompanyNameAsync(string name);
        public Task AddCustomerAsync(Customer customer);
        public Task DeleteCustomerAsync(Customer customer);
        public Task UpdateCustomerAsync(Customer customer);
        public bool CustomerExists(string id);
    }
}
