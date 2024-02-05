using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using System.Net.Http.Json;

namespace PhotoBrowser.Services
{
    public class ClientService : IClientService
    {
        private HttpClient Http { get; set; }

        public ClientService(HttpClient http) { 
            Http = http;
        }

        public async Task<List<User>> GetUsers()
        {
            CancellationToken cancellationToken = new();
            try
            {
                var response = await Http.GetFromJsonAsync<List<User>>("https://jsonplaceholder.typicode.com/users", cancellationToken);
                if (response is not null)
                {
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            return [];
        }

        public async Task<List<Album>> GetAlbums()
        {
            CancellationToken cancellationToken = new();
            try
            {
                var response = await Http.GetFromJsonAsync<List<Album>>("https://jsonplaceholder.typicode.com/albums", cancellationToken);
                if (response is not null)
                {
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            return [];
        }

        public async Task<List<Photo>> GetPhotos()
        {
            CancellationToken cancellationToken = new();
            try
            {
                var response = await Http.GetFromJsonAsync<List<Photo>>("https://jsonplaceholder.typicode.com/photos", cancellationToken);
                if (response is not null)
                {
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            return [];
        }
    }
}
