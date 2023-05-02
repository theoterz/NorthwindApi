using NorthwindModels.DTOs;
using NorthwindUIBL.Interfaces;
using System.Net.Http.Json;

namespace NorthwindUIBL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(CustomerCreateDTO entity)
        {
            var reply = await _httpClient.PostAsJsonAsync<CustomerCreateDTO>("api/Customer", entity);

            if (reply.IsSuccessStatusCode) return true;
            return false;

        }

        public async Task<bool> Delete(string id)
        {
            var result = await _httpClient.DeleteAsync($"api/Customer/{id}");

            if (result.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<IEnumerable<CustomerDTO>?> GetAll()
        {
            var reply = await _httpClient.GetAsync($"api/Customer");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>();
            else return null;
        }

        public async Task<IEnumerable<CustomerDTO>?> GetByCompanyName(string companyName)
        {
            var reply = await _httpClient.GetAsync($"api/Customer/CompanyName/{companyName}");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>();
            else return null;
        }

        public async Task<CustomerDTO?> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"api/Customer/{id}");

            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<CustomerDTO>();
            else return null;
        }

        public async Task<bool> UpdateCustomer(CustomerDTO customer)
        {
            var result = await _httpClient.PutAsJsonAsync<CustomerDTO>("api/Customer", customer);

            if (result.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
