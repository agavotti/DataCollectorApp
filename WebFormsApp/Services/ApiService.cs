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

        public async Task<Album> AddAlbum(Album album)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7122/api/albums", album);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var createdAlbum = await response.Content.ReadFromJsonAsync<Album>(options);
                return createdAlbum;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                errorMessage = errorMessage.Split(":")[1];
                errorMessage = errorMessage.Replace("\"","").Replace("}","");

                throw new Exception($"Error al crear el álbum: {errorMessage}");
            }
        }

        public async Task<Photo> AddPhoto(Photo photo)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7122/api/photos", photo);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var createdPhoto = await response.Content.ReadFromJsonAsync<Photo>(options);
                return createdPhoto;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync(); 
                errorMessage = errorMessage.Split(":")[1];
                errorMessage = errorMessage.Replace("\"", "").Replace("}", "");

                throw new Exception($"Error al crear la foto: {errorMessage}");
            }
        }

        public async Task<List<Album>> GetStoredAlbumsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7122/api/Albums/");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Album>>(response, options);
        }

        public async Task<List<Photo>> GetStoredPhotosAsync()
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7122/api/Photos");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<Photo>>(response, options);
        }
    }
}
