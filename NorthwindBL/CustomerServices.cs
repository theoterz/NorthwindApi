using AutoMapper;
using NorthwindDAL;
using NorthwindDAL.Repositories;
using NorthwindModels.DTOs;
using NorthwindModels.Models;

namespace NorthwindBL
{
    public class CustomerServices : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerServices(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
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

            if (_dbContext.Customers.Any(c => c.CustomerID.Equals(customerDTO.CustomerID))) return null;

            _dbContext.Add(_mapper.Map<Customer>(customerDTO));
            _dbContext.SaveChanges();

            return customerDTO;
        }

        /// <summary>
        /// Checks if the object the user wants to delete exists and then it deletes it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool DeleteCustomer(string id)
        {
            Customer? customer = _dbContext.Customers.Find(id);

            if (customer is null) return false;

            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return _dbContext.Customers.Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public IEnumerable<CustomerDTO> GetByCompanyName(string name)
        {
            return _dbContext.Customers.Where(c => c.CompanyName.Contains(name)).Select(c => _mapper.Map<CustomerDTO>(c));
        }

        public CustomerDTO? GetById(string id)
        {
            return _mapper.Map<CustomerDTO>(_dbContext.Customers.Find(id));
        }

        /// <summary>
        /// If the id is correct, the method updates the customer.
        /// </summary>
        /// <param name="updatedCustomer"></param>
        /// <returns>The method returns if the operation was successful or not</returns>
        public bool UpdateCustomer(CustomerUpdateDTO updatedCustomer)
        {
            if (_dbContext.Customers.Any(c => c.CustomerID.Equals(updatedCustomer.CustomerID)))
            {
                Customer customerToUpdate = _mapper.Map<Customer>(updatedCustomer);

                _dbContext.Customers.Update(customerToUpdate);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
