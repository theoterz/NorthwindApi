using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Products
{
    public partial class CreateProduct
    {
        [Inject]
        private IProductService _productService { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

   
        private ProductCreateDTO _productCreateDTO = new ProductCreateDTO();      
        private string _message = string.Empty;

        private async Task Create(Object product)
        {
            _message = string.Empty;
            _message = await _productService.Create((ProductCreateDTO)product);
            if (_message.Equals("Success")) _navigationManager.NavigateTo("/Products");
        }
    }
}
