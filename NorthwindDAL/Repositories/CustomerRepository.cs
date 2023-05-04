using Microsoft.EntityFrameworkCore;
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

        public async Task AddCustomerAsync(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _appDbContext.Customers.ToListAsync();
        }

        public async Task<List<Customer>?> GetByCompanyNameAsync(string name)
        {
            return await _appDbContext.Customers.Where(c => c.CompanyName.Contains(name)).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(string id)
        {
            return await _appDbContext.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _appDbContext.Customers.Update(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public bool CustomerExists(string id)
        {
            return _appDbContext.Customers.Any(c => c.CustomerID.Equals(id));
        }
    }
}
