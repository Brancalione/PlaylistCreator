//using Newtonsoft.Json;
//using ServiceWeb.Models;
//using System.Net.Http.Headers;

//namespace ServiceWeb.Services
//{
//    public class BuscaIdMusicas
//    {
//        private readonly HttpClient _client;

//        public BuscaIdMusicas()
//        {
//            _client = new HttpClient();
//        }

//        public async Task<string> GetIdMusicAsync(string nomeMusica, string token)
//        {
//            try
//            {

//                nomeMusica = nomeMusica.Replace(" - ", " artist:").Replace(" ", "%20");
//                string url = "https://api.spotify.com/v1/search?q=track:" + nomeMusica + "&type=track&market=BR&limit=1&offset=0";

//                var request = new HttpRequestMessage(HttpMethod.Get, url);
//                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


//                var response = await _client.SendAsync(request);

//                if (response.IsSuccessStatusCode)
//                {
//                    var jsonResponse = await response.Content.ReadAsStringAsync();
//                    var tracksResponse = JsonConvert.DeserializeObject<Tracks>(jsonResponse);
//                    return tracksResponse.tracks.items[0].uri;
//                }
//                else
//                {
//                    return null;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Erro buscar música no Chatgpt {ex.Message}");
//                return null;
//            }
//        }
//    }
//}

using Newtonsoft.Json;
using ServiceWeb.Models;
using System.Net.Http.Headers;

namespace ServiceWeb.Services
{
    public class BuscaIdMusicas
    {
        private readonly HttpClient _httpClient;

        public BuscaIdMusicas()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetIdMusicAsync(string nomeMusica, string token)
        {
            
            try
            {
                string url = ConstruirUrlDeBusca(nomeMusica);
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode(); // Lança exceção em caso de erro HTTP.

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var trackUri = ExtrairUriDaMusica(jsonResponse);

                return trackUri ?? throw new Exception("Nenhuma música encontrada.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de requisição: {ex.Message}");
                return "";
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erro ao processar resposta JSON: {ex.Message}");
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return "";
            }
        }

        private string ConstruirUrlDeBusca(string nomeMusica)
        {
            string query = nomeMusica.Replace(" - ", " artist:").Replace(" ", "%20");
            return $"https://api.spotify.com/v1/search?q=track:{query}&type=track&market=BR&limit=1&offset=0";
        }

        private string ExtrairUriDaMusica(string jsonResponse)
        {
            var tracksResponse = JsonConvert.DeserializeObject<Tracks>(jsonResponse);
            return tracksResponse?.tracks?.items?.FirstOrDefault()?.uri;
        }
    }
}
