using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceWeb.Models;
using System.Net.Http.Headers;

namespace ServiceWeb.Services
{
    public class SpotifyCallback

    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public SpotifyCallback(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
        }

        public async Task<ResponseSpotifyAuthUserModel> GetTokenUserAsync(string code)
        {

            string clientId = _configuration["ApiKeys:clientId"];
            string clientSecret = _configuration["ApiKeys:clientSecret"];

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            //Criar propriedade do cabeçalho x-www-form-urlencoded
            var postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "https://localhost:7133/api/SpotifyCallback/callback"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            });

            try
            {
                request.Content = postData;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseSpotifyAuthUserModel>(jsonResponse);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                    return null;
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