using NorthwindModels.DTOs;
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
            var result = await _httpClient.PostAsJsonAsync("api/Product", productDTO);
            string message = string.Empty;

            if (result.IsSuccessStatusCode) message = "Success";
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await result.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = "Validation Error";
            }
            return message;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Product/{id}");

            if (result.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<IEnumerable<ProductDTO>?> GetAll()
        {
            var reply = await _httpClient.GetAsync($"api/Product");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>();
            else return null;
        }

        public async Task<ProductDTO?> GetById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<ProductDTO>();
            else return null;
        }

        public async Task<string> Update(ProductDTO productDTO)
        {
            var result = await _httpClient.PutAsJsonAsync<ProductDTO>("api/Product", productDTO);
            string message = string.Empty;

            if (result.IsSuccessStatusCode) message = "Success";
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await result.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = "Validation Error";
            }
            return message;
        }
    }
}
