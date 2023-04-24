using AutoMapper;
using NorthwindDAL.Interfaces;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL
{
    public class CustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerServices(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        /// <summary>
        /// Checks if a Customer with the same id exists in the database. If not, the method adds the Customer in the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>The method returns the created customer. If a null value is returned, the customer's id already exists.</returns>
        public CustomerCreateDTO? AddCustomer(CustomerCreateDTO customerDTO)
        {
            customerDTO.CustomerID = customerDTO.CustomerID.ToUpper();

            if (_customerRepository.CustomerExists(customerDTO.CustomerID)) return null;

            Customer newCustomer = _mapper.Map<Customer>(customerDTO);
            _customerRepository.AddCustomer(newCustomer);

            return customerDTO;
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool DeleteCustomer(string id)
        {
            Customer? customer = _customerRepository.GetById(id);

            if (customer is null) return false;

            _customerRepository.DeleteCustomer(customer);

            return true;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            IEnumerable<Customer> customers = _customerRepository.GetAll();
            return customers.Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public IEnumerable<CustomerDTO> GetByCompanyName(string name)
        {
            IEnumerable<Customer> customers = _customerRepository.GetByCompanyName(name);
            return customers.Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public CustomerDTO? GetById(string id)
        {
            Customer? customer = _customerRepository.GetById(id);
            return _mapper.Map<CustomerDTO>(customer);
        }

        /// <summary>
        /// If the id is correct, the method updates the customer.
        /// </summary>
        /// <param name="updatedCustomer"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool UpdateCustomer(CustomerDTO customerDTO)
        {

            if (!_customerRepository.CustomerExists(customerDTO.CustomerID)) return false;

            Customer customerToUpdate = _mapper.Map<Customer>(customerDTO);
            _customerRepository.UpdateCustomer(customerToUpdate);

            return true;

        }
    }
}
