using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;
using NorthwindUIBL.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace NorthwindUIBL.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Create(ProductCreateDTO productDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Products", productDTO);
            string message = string.Empty;

            if (result.IsSuccessStatusCode) message = ProductErrorMessages.Success;
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await result.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = ProductErrorMessages.ValidationError;
            }
            return message;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Products/{id}");

            if (result.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<IEnumerable<ProductDTO>?> GetAll()
        {
            var reply = await _httpClient.GetAsync($"api/Products");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
            else return null;
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");

            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<ProductDTO>();
            else return null;
        }

        public async Task<string> Update(ProductDTO productDTO)
        {
            var result = await _httpClient.PutAsJsonAsync<ProductDTO>("api/Products", productDTO);
            string message = string.Empty;

            if (result.IsSuccessStatusCode) message = ProductErrorMessages.Success;
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await result.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = ProductErrorMessages.ValidationError;
            }
            return message;
        }
    }
}
