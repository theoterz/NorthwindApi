using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Customers
{
    public partial class CustomerList
    {
        [Inject]
        private ICustomerService _customerService { get; set; } = null!;

        private List<CustomerDTO>? customers;
        
        private bool searchById = true;
        private bool _showGetAllCustomersBtn = false;
        
        //Search By Id Variables
        private bool _showSearchByIdLabel = false;
        private string _customerId = string.Empty;

        //Search By Company Name Variables
        private bool _showSearchByCompanyNameLabel = false;
        private string _companyName = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            IEnumerable<CustomerDTO>? fetchedCustomers = await _customerService.GetAll();
            if (fetchedCustomers is not null) customers = fetchedCustomers.ToList();
            else customers = new List<CustomerDTO>();
        }

        private async Task DeleteCustomer(object id)
        {
           bool result = await _customerService.Delete(id.ToString()!);
           if (result) await GetAllCustomers();
        }

        private async Task SearchById()
        {
            customers!.Clear();
            _showSearchByIdLabel = false;
            _showGetAllCustomersBtn = true;

            CustomerDTO? customer = await _customerService.GetById(_customerId);
            if (customer is null) _showSearchByIdLabel = true;
            else customers?.Add(customer);
          
            _customerId = string.Empty;
        }

        private async Task SearchByCompanyName()
        {
            _showSearchByCompanyNameLabel = false;
            _showGetAllCustomersBtn = true;

            customers!.Clear();

            IEnumerable<CustomerDTO>? fetchedCustomers = await _customerService.GetByCompanyName(_companyName);
            if (fetchedCustomers is not null) customers.AddRange(fetchedCustomers);
            else _showSearchByCompanyNameLabel= true;

            _companyName = string.Empty;
        }

        private async Task GetAllCustomers()
        {
            _showGetAllCustomersBtn = false;
            _showSearchByIdLabel = false;
            _showSearchByCompanyNameLabel = false;

            customers!.Clear();

            IEnumerable<CustomerDTO>? fetchedCustomers = await _customerService.GetAll();
            if (fetchedCustomers is not null) customers.AddRange(fetchedCustomers);
        }
    }
}
