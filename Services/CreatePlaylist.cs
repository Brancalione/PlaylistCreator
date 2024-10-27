using Newtonsoft.Json;
using ServiceWeb.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWeb.Services
{
    public class SpotifyPlaylist
    {
        private readonly HttpClient _client;

        public SpotifyPlaylist()
        {
            _client = new HttpClient();
        }

        public async Task<ResonseCreatePlaylistModel> CreatePlaylistAsync(string token)
        {
            string url = "https://api.spotify.com/v1/users/jeova-brancalione/playlists";

            // Criação do corpo da requisição JSON
            var bodyRequest = new
            {
                name = "Playlist API",
                description = "New playlist description",
                @public = true
            };

            // Serializando o corpo para JSON
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(bodyRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResonseCreatePlaylistModel>(jsonResponse);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar a playlist: {ex.Message}");
                return null;
            }
        }
    }
}