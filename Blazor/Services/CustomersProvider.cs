using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class CustomersProvider :ICustomersProvider
    {
        private HttpClient _client;
        public CustomersProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Customer>>("/apiclient");
        }

        public async Task<Customer> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Customer>($"/apiclient/{id}");
        }

        public async Task<bool> Add(Customer ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/apiclient", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }

        public async Task<Customer> Edit(Customer ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/apiclient", httpContent);
            Customer client = JsonConvert.DeserializeObject<Customer>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(client);
        }

        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/apiclient/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}

