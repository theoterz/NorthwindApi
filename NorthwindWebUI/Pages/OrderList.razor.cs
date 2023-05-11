using Microsoft.AspNetCore.Components;
using NorthwindComponentLibrary;
using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;
using NorthwindUIBL.Interfaces;
using Radzen;
using Radzen.Blazor;

namespace NorthwindWebUI.Pages
{
    public partial class OrderList
    {
        [Inject]
        private IOrderService OrderService { get; set; } = null!;
        [Inject]
        private DialogService DialogService { get; set; } = null!;
        [Inject]
        private NotificationService NotificationService { get; set; } = null!;

        private IEnumerable<OrderDTO>? _orders;
        private RadzenDataGrid<OrderDTO>? _ordersGrid;
        private bool _dataAreLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            _orders = await OrderService.GetAll();
            _dataAreLoaded = true;
        }

        private async Task OnCreate()
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Create },
                { "Entity", new OrderCreateDTO() },
                { "EntityType", typeof(OrderCreateDTO) },
                { "Operation", EventCallback.Factory.Create<object>(this, CreateOrder) }
            };

            await DialogService.OpenAsync<EntityForm>("Create Order", parameters);
        }

        private async Task CreateOrder(Object newOrder)
        {
            string result = await OrderService.Create((OrderCreateDTO)newOrder);

            if (result.Equals(OrderErrorMessages.Success))
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, OrderErrorMessages.OrderCreated);
                await GetAllOrders();
                _ordersGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, result);

        }

        private async Task OnRowClick(OrderDTO order)
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Edit },
                { "Entity", order.Clone() },
                { "EntityType", typeof(OrderDTO) },
                { "Operation", EventCallback.Factory.Create<object>(this, UpdateOrder) }
            };

            await DialogService.OpenAsync<EntityForm>("Edit Order", parameters);
        }

        private async Task UpdateOrder(Object order)
        {
            string result = await OrderService.Update((OrderDTO)order);

            if (result.Equals(OrderErrorMessages.Success))
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, OrderErrorMessages.OrderUpdated);
                await GetAllOrders();
                _ordersGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, result);
        }

        private async Task GetAllOrders()
        {
            _orders = await OrderService.GetAll();
        }
        private async Task DeleteOrder(int id)
        {
            var confirmed = await DialogService.Confirm("Are you sure you want to delete this Order?", "Delete Order");

            if (confirmed.HasValue && confirmed.Value)
            {
                bool result = await OrderService.Delete(id);
                if (result)
                {
                    await GetAllOrders();
                    _ordersGrid?.Reload();
                    NotificationService.Notify(NotificationSeverity.Success, OrderErrorMessages.OrderDeleted);
                }
                else NotificationService.Notify(NotificationSeverity.Error, OrderErrorMessages.DeletionError);
            }
        }
    }
}
