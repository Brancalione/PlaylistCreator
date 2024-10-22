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

        public async Task CreatePlaylistAsync(string token)
        {
            string url = "https://api.spotify.com/v1/users/jeova-brancalione/playlists";

            // Criação do corpo da requisição JSON
            var bodyRequest = new
            {
                name = "New Playlist",
                description = "New playlist description",
                @public = false
            };

            // Serializando o corpo para JSON
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(bodyRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                // Envio da requisição HTTP POST
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Playlist criada com sucesso!");
                    var responseBody = await response.Content.ReadAsStringAsync();
                   return; // Exibe a resposta da API
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.StatusCode}, Details: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar a playlist: {ex.Message}");
            }
            return;
        }
    }
}