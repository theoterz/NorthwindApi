using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Orders
{
    public partial class CreateOrder
    {
        [Inject]
        private IOrderService _orderService { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private OrderCreateDTO _orderCreateDTO { get; set; } = new OrderCreateDTO();
        private string _message = string.Empty;

        private async Task Create()
        {
            _message = string.Empty;
            _message = await _orderService.Create(_orderCreateDTO);
            if (_message.Equals("Success")) _navigationManager.NavigateTo("/Orders");
        }
    }
}
