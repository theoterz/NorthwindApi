using Microsoft.AspNetCore.Components;
using NorthwindModels.DTOs;
using NorthwindWebUI.Services;

namespace NorthwindWebUI.Pages.Products
{
    public partial class ProductList
    {
        [Inject]
        private IProductService _productService { get; set; } = null!;

        private List<ProductDTO>? _products;

        //Search by Id
        private string _productId = string.Empty;
        private string? _searchByIdErrorMessage;
        private bool _showSearchByIdLabel = false;
        private bool _showGetAllProductsBtn = false;

        protected override async Task OnInitializedAsync()
        {
            IEnumerable<ProductDTO>? fetchedProducts = await _productService.GetAll();
            if (fetchedProducts is not null) _products = fetchedProducts.ToList();
            else _products = new List<ProductDTO>();
        }

        private async Task SearchById()
        {
            _searchByIdErrorMessage = string.Empty;
            _showSearchByIdLabel = false;

            if (_productId is not null)
            {
                if (Int32.TryParse(_productId, out int id) && id > 0)
                {

                    _products!.Clear();
                    ProductDTO? fetchedProduct = await _productService.GetById(id);

                    if (fetchedProduct is not null) _products.Add(fetchedProduct);
                    else
                    {
                        _showSearchByIdLabel = true;
                        _searchByIdErrorMessage = "No Order matches this id";
                    }

                    _showGetAllProductsBtn = true;
                }
                else
                {
                    _showSearchByIdLabel = true;
                    _searchByIdErrorMessage = "The Order id should be a positive integer";
                }
            }

            _productId = string.Empty;

        }

        private async Task DeleteProduct(object id)
        {
            bool result = await _productService.Delete(Convert.ToInt32(id));
            if (result) await GetAllProducts();
        }

        private async Task GetAllProducts()
        {
            _showSearchByIdLabel = false;
            _showGetAllProductsBtn = false;

            _products!.Clear();
            IEnumerable<ProductDTO>? fetchedProducts = await _productService.GetAll();
            if (fetchedProducts is not null) _products.AddRange(fetchedProducts);
        }
    }
}
