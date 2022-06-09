using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class TicketsProvider : ITicketsProvider
    {
        private HttpClient _client;
        public TicketsProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Ticket>>("/apiticket");
        }

        public async Task<Ticket> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Ticket>($"/apiticket/{id}");
        }

        public async Task<bool> Add(Ticket ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/apiticket", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }

        public async Task<Ticket> Edit(Ticket ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/apiticket", httpContent);
            Ticket item1 = JsonConvert.DeserializeObject<Ticket>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(item1);
        }

        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/apiticket/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}

