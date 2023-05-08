using NorthwindModels.DTOs;

namespace NorthwindUIBL.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerDTO>?> GetAll();
        public Task<CustomerDTO?> GetById(string id);
        public Task<bool> Delete(string id);
        public Task<string> Create(CustomerCreateDTO entity);
        public Task<IEnumerable<CustomerDTO>?> GetByCompanyName(string companyName);
        public Task<string> UpdateCustomer(CustomerDTO customer);
    }
}
