using AutoMapper;
using NorthwindBL.Interfaces;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly IEntityRepository<Customer, string> _customerRepository;
        private readonly IMapper _mapper;
        public CustomerServices(IEntityRepository<Customer, string> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Checks if a Customer with the same id exists in the database. If not, the method adds the Customer in the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>The method returns the created customer. If a null value is returned, the customer's id already exists.</returns>
        public async Task<CustomerCreateDTO?> AddCustomerAsync(CustomerCreateDTO customerDTO)
        {
            customerDTO.CustomerID = customerDTO.CustomerID.ToUpper();

            if (_customerRepository.EntityExists(customerDTO.CustomerID)) return null;

            Customer newCustomer = _mapper.Map<Customer>(customerDTO);
            await _customerRepository.CreateEntityAsync(newCustomer);

            return customerDTO;
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public async Task<bool> DeleteCustomerAsync(string id)
        {
            Customer? customer = await _customerRepository.GetEntityByIdAsync(id);

            if (customer is null) return false;

            await _customerRepository.DeleteEntityAsync(customer);

            return true;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllEntitiesAsync();
            return customers.Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public async Task<IEnumerable<CustomerDTO>?> GetByCompanyNameAsync(string name)
        {
            IEnumerable<Customer>? customers = await _customerRepository.GetAllEntitiesAsync(c => c.CompanyName.Contains(name));
            return customers?.Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public async Task<CustomerDTO?> GetByIdAsync(string id)
        {
            Customer? customer = await _customerRepository.GetEntityByIdAsync(id);
            return _mapper.Map<CustomerDTO>(customer);
        }

        /// <summary>
        /// If the id is correct, the method updates the customer.
        /// </summary>
        /// <param name="updatedCustomer"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO)
        {

            if (!_customerRepository.EntityExists(customerDTO.CustomerID)) return false;

            Customer customerToUpdate = _mapper.Map<Customer>(customerDTO);
            await _customerRepository.UpdateEntityAsync(customerToUpdate);

            return true;
        }
    }
}
