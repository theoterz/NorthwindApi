using NorthwindModels.DTOs;

namespace NorthwindBL.Interfaces
{
    public interface ICustomerServices
    {
        Task<CustomerCreateDTO?> AddCustomerAsync(CustomerCreateDTO customerDTO);
        Task<bool> DeleteCustomerAsync(string id);
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<IEnumerable<CustomerDTO>?> GetByCompanyNameAsync(string name);
        Task<CustomerDTO?> GetByIdAsync(string id);
        Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO);
    }
}