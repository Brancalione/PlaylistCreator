using Newtonsoft.Json;
using ServiceWeb.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ServiceWeb.Services 
{
    public class SpotifyAuth
    {
        private readonly HttpClient _client;

        public SpotifyAuth()
        {
            _client = new HttpClient();
        }

        public async Task<ResponseSpotifyAuthModel> GetTokenAsync()
        {
            string clientId = "";
            string clientSecret = "";

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            //Criar propriedade do cabeçalho x-www-form-urlencoded
            var postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            });

            request.Content = postData;
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseSpotifyAuthModel>(jsonResponse);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }
}