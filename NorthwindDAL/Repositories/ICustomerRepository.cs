using NorthwindModels.DTOs;

namespace NorthwindDAL.Repositories
{
    public interface ICustomerRepository
    {
        public IEnumerable<CustomerDTO> GetAll();
        public CustomerDTO? GetById(string id);
        public IEnumerable<CustomerDTO> GetByCompanyName(string name);
        public CustomerCreateDTO? AddCustomer(CustomerCreateDTO customerDTO);
        public bool DeleteCustomer(string id);
        public bool UpdateCustomer(CustomerUpdateDTO customerDTO);
    }
}
