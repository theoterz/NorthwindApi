using Microsoft.AspNetCore.Components;
using NorthwindComponentLibrary;
using NorthwindModels.DTOs;
using NorthwindUIBL.Interfaces;
using Radzen;
using Radzen.Blazor;

namespace NorthwindWebUI.Pages
{
    public partial class CustomerList
    {
        [Inject]
        private ICustomerService CustomerService { get; set; } = null!;
        [Inject]
        private DialogService DialogService { get; set; } = null!;
        [Inject]
        private NotificationService NotificationService { get; set; } = null!;

        private IEnumerable<CustomerDTO>? _customers;
        private RadzenDataGrid<CustomerDTO>? _customersGrid;
        private bool _dataAreLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            _customers = await CustomerService.GetAll();
            _dataAreLoaded = true;
        }

        private async Task OnCreate()
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Create },
                { "Entity", new CustomerCreateDTO() },
                { "EntityType", typeof(CustomerCreateDTO)},
                { "Operation", EventCallback.Factory.Create<object>(this, CreateCustomer) }
            };

            await DialogService.OpenAsync<EntityForm>("Create Customer", parameters);
        }

        private async Task CreateCustomer(Object newCustomer)
        {
            bool result = await CustomerService.Create((CustomerCreateDTO)newCustomer);
            
            if(result)
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, "The Customer has been created!");
                await GetAllCustomers();
                _customersGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, "The Customer has not been created");
        }

        private async Task OnRowClick(CustomerDTO customer)
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Edit },
                { "Entity", customer.Clone() },
                { "EntityType", typeof(CustomerDTO) },
                { "Operation", EventCallback.Factory.Create<object>(this, UpdateCustomer) }
            };
 
            await DialogService.OpenAsync<EntityForm>("Edit Customer", parameters);
        }

        private async Task UpdateCustomer(Object customer)
        {
            bool result = await CustomerService.UpdateCustomer((CustomerDTO)customer);

            if(result)
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, "The Customer has been updated!");
                await GetAllCustomers();
                _customersGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, "An error occured during the update!");
        }

        private async Task DeleteCustomer(string id)
        {
            var confirmed = await DialogService.Confirm("Are you sure you want to delete this Customer?", "Delete Customer");

            if (confirmed.HasValue && confirmed.Value)
            {
                bool result = await CustomerService.Delete(id);
                if (result)
                {
                    await GetAllCustomers();
                    _customersGrid?.Reload();
                    NotificationService.Notify(NotificationSeverity.Success, "The Customer is successfuly deleted!");
                }
                else NotificationService.Notify(NotificationSeverity.Error, "An error occured during the deletion!");
            }
        }

        private async Task GetAllCustomers()
        {
            _customers = await CustomerService.GetAll();
        }
    }
}
