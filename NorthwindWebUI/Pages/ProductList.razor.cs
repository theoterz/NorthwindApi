using Microsoft.AspNetCore.Components;
using NorthwindComponentLibrary;
using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;
using NorthwindUIBL.Interfaces;
using Radzen;
using Radzen.Blazor;

namespace NorthwindWebUI.Pages
{
    public partial class ProductList
    {
        [Inject]
        private IProductService ProductService { get; set; } = null!;
        [Inject]
        private DialogService DialogService { get; set; } = null!;
        [Inject]
        private NotificationService NotificationService { get; set; } = null!;

        private IEnumerable<ProductDTO>? _products;
        private RadzenDataGrid<ProductDTO>? _productsGrid;
        private bool _dataAreLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            _products = await ProductService.GetAll();
            _dataAreLoaded = true;
        }

        private async Task OnCreate()
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Create },
                { "Entity", new ProductCreateDTO() },
                { "EntityType", typeof(ProductCreateDTO) },
                { "Operation", EventCallback.Factory.Create<object>(this, CreateProduct) }
            };

            await DialogService.OpenAsync<EntityForm>("Create Product", parameters);
        }

        private async Task CreateProduct(Object newProduct)
        {
            string result = await ProductService.Create((ProductCreateDTO)newProduct);

            if (result.Equals(ProductErrorMessages.Success))
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, ProductErrorMessages.ProductCreated);
                await GetAllProducts();
                _productsGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, result);
        }

        private async Task OnRowClick(ProductDTO product)
        {
            Dictionary<string, object> parameters = new()
            {
                { "OperationName", EntityForm.OperationType.Edit },
                { "Entity", product.Clone() },
                { "EntityType", typeof(ProductDTO) },
                { "Operation", EventCallback.Factory.Create<object>(this, UpdateProduct) }
            };

            await DialogService.OpenAsync<EntityForm>("Edit Product", parameters);
        }

        private async Task UpdateProduct(Object product)
        {
            string result = await ProductService.Update((ProductDTO)product);

            if (result.Equals(ProductErrorMessages.Success))
            {
                DialogService.Close();
                NotificationService.Notify(NotificationSeverity.Success, ProductErrorMessages.ProductUpdated);
                await GetAllProducts();
                _productsGrid?.Reload();
            }
            else NotificationService.Notify(NotificationSeverity.Error, result);
        }

        private async Task DeleteProduct(int id)
        {

            var confirmed = await DialogService.Confirm("Are you sure you want to delete this Product?", "Delete Product");

            if (confirmed.HasValue && confirmed.Value)
            {
                bool result = await ProductService.Delete(id);
                if (result)
                {
                    await GetAllProducts();
                    _productsGrid?.Reload();
                    NotificationService.Notify(NotificationSeverity.Success, ProductErrorMessages.ProductDeleted);
                }
                else NotificationService.Notify(NotificationSeverity.Error, ProductErrorMessages.DeletionError);
            }
        }

        private async Task GetAllProducts()
        {
            _products = await ProductService.GetAll();
        }
    }
}
