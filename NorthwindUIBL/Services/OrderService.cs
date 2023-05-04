using NorthwindModels.DTOs;
using NorthwindUIBL.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace NorthwindUIBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Create(OrderCreateDTO orderDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Orders", orderDTO);
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
            var result = await _httpClient.DeleteAsync($"api/Orders/{id}");

            if (result.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<IEnumerable<OrderDTO>?> GetAll()
        {
            var reply = await _httpClient.GetAsync($"api/Orders");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            else return null;
        }

        public async Task<IEnumerable<OrderDTO>?> GetByCustomerAndEmployee(string customerId, int employeeId)
        {
            var reply = await _httpClient.GetAsync($"api/Orders/getByCustomerAndEmployee/{customerId}/{employeeId}");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            else return null;
        }

        public async Task<IEnumerable<OrderDTO>?> GetByCustomerId(string customerId)
        {
            var reply = await _httpClient.GetAsync($"api/Orders/getByCustomer/{customerId}");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
            else return null;
        }

        public async Task<OrderDTO?> GetById(int id)
        {
            var reply = await _httpClient.GetAsync($"api/Orders/{id}");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<OrderDTO>();
            else return null;
        }

        public async Task<string> Update(OrderDTO orderDTO)
        {
            var result = await _httpClient.PutAsJsonAsync<OrderDTO>($"api/Orders", orderDTO);
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
