using NorthwindModels.DTOs;
using NorthwindModels.ErrorMessages;
using NorthwindUIBL.Interfaces;
using System.Net;
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

        public async Task<string> Create(CustomerCreateDTO entity)
        {
            var reply = await _httpClient.PostAsJsonAsync<CustomerCreateDTO>("api/Customers", entity);
            string message = string.Empty;

            if (reply.IsSuccessStatusCode) message = CustomerErrorMessages.Success;
            else if (reply.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await reply.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = CustomerErrorMessages.ValidationError;
            }
            return message;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _httpClient.DeleteAsync($"api/Customers/{id}");

            if (result.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<IEnumerable<CustomerDTO>?> GetAll()
        {
            var reply = await _httpClient.GetAsync($"api/Customers");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>();
            else return null;
        }

        public async Task<IEnumerable<CustomerDTO>?> GetByCompanyName(string companyName)
        {
            var reply = await _httpClient.GetAsync($"api/Customers/CompanyName/{companyName}");

            if (reply.IsSuccessStatusCode) return await reply.Content.ReadFromJsonAsync<IEnumerable<CustomerDTO>>();
            else return null;
        }

        public async Task<CustomerDTO?> GetById(string id)
        {
            var response = await _httpClient.GetAsync($"api/Customers/{id}");

            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<CustomerDTO>();
            else return null;
        }

        public async Task<string> UpdateCustomer(CustomerDTO customer)
        {
            var result = await _httpClient.PutAsJsonAsync<CustomerDTO>("api/Customers", customer);
            string message = string.Empty;

            if (result.IsSuccessStatusCode) message = CustomerErrorMessages.Success;
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorStream = await result.Content.ReadAsStreamAsync();
                message = new StreamReader(errorStream).ReadToEnd();

                if (message.Contains("validation")) message = CustomerErrorMessages.ValidationError;
            }

            return message;
        }
    }
}
