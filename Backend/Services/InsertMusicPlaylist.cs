using Newtonsoft.Json;
using ServiceWeb.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServiceWeb.Services
{
    public class InsertMusicPlaylist
    {
        private readonly HttpClient _client;

        public InsertMusicPlaylist()
        {
            _client = new HttpClient();
        }

        public async Task InserirMusicPlaylistAsync(string idMusica, string token, int l_i, string idPlaylist)
        {
            string idMusicaEscaped = Uri.EscapeDataString($"spotify:track:{idMusica}");// Remove : da URL e insere o formato correto %2
            string url = $"https://api.spotify.com/v1/playlists/{idPlaylist}/tracks";


            BodySpotifyModel bodyRequest = new BodySpotifyModel();
            bodyRequest.uris = new string[1];
            bodyRequest.uris[0] = idMusica;
            bodyRequest.position = l_i;

            var jsonBody = JsonConvert.SerializeObject(bodyRequest);
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");


            try
            {
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.StatusCode}, Details: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir a música na playlist: {ex.Message}");
                throw; 
            }
        }
    }
}
