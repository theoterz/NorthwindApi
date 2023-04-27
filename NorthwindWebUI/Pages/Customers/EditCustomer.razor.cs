using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Customers
{
    public partial class EditCustomer
    {
        [Parameter]
        public string Id { get; set; } = null!;
        [Inject]
        private ICustomerService _customerService { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private CustomerDTO? _customer;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _customer = await _customerService.GetById(Id);
        }

        private async Task UpdateCustomer(Object customerToUpdate)
        {
            bool result = await _customerService.UpdateCustomer((CustomerDTO)customerToUpdate);

            if (result) _navigationManager.NavigateTo("/Customers");
            else _errorMessage = "No Customer found!";
        }
    }
}
