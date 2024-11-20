using Domain.Entities;
using System.Text.Json;

namespace WebFormsApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Album>> GetAlbumsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7122/api/ExternalAlbums/Albums");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Album>>(response, options); ;
        }

        public async Task<List<Photo>> GetPhotos()
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7122/api/ExternalAlbums/Photos");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Photo>>(response, options);
        }
    }
}
