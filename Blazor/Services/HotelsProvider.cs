using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class HotelsProvider : IHotelProvider
    {
        private HttpClient _client;
        public HotelsProvider(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Hotel>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Hotel>>("/apihotel");
        }
        public async Task<Hotel> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Hotel>($"/apihotel/{id}");
        }
        public async Task<bool> Add(Hotel ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/apihotel", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }
        public async Task<Hotel> Edit(Hotel ticket)
        {
            string data = JsonConvert.SerializeObject(ticket);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/apihotel", httpContent);
            Hotel hotel = JsonConvert.DeserializeObject<Hotel>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(hotel);
        }
        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/apihotel/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}