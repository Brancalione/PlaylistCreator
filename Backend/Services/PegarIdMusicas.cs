

using Newtonsoft.Json;
using ServiceWeb.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ServiceWeb.Services
{
    public class BuscaIdMusicas
    {
        private readonly HttpClient _client;

        public BuscaIdMusicas()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetIdMusicAsync(string nomeMusica, string token)
        {
            nomeMusica = nomeMusica.Replace(" - ", " artist:").Replace(" ", "%20");
            string url = "https://api.spotify.com/v1/search?q=track:" + nomeMusica + "&type=track&market=BR&limit=1&offset=0";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tracksResponse = JsonConvert.DeserializeObject<Tracks>(jsonResponse);
                return tracksResponse.tracks.items[0].uri;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}