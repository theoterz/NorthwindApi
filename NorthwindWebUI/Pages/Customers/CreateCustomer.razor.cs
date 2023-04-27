using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Customers
{
    public partial class CreateCustomer
    {
        [Inject]
        private ICustomerService _service { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private CustomerCreateDTO Customer = new CustomerCreateDTO();

        private async Task Create(Object newCustomer)
        {
            bool result = await _service.Create((CustomerCreateDTO)newCustomer);

            if (result) _navigationManager.NavigateTo("/Customers");
        }
    }

}
