using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Products
{
    public partial class EditProduct
    {
        [Parameter]
        public string Id { get; set; } = null!;
        [Inject]
        private IProductService _productService { get; set; } = null!;
        [Inject]
        private NavigationManager _navigationManager { get; set; } = null!;

        private ProductDTO? product;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            product = await _productService.GetById(int.Parse(Id));
        }

        private async Task UpdateProduct(Object productToUpdate)
        {
            _errorMessage = string.Empty;
            _errorMessage = await _productService.Update((ProductDTO)productToUpdate);
            if (_errorMessage.Equals("Success")) _navigationManager.NavigateTo("/Products");
        }
    }
}
