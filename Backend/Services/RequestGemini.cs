using Newtonsoft.Json;
using ServiceWeb.Models;
using System.Net.Http.Headers;
using System.Text;


namespace ServiceWeb.Services
{
    public class RequestGemini
    {
        private readonly HttpClient _client;
        //private readonly IConfiguration _configuration;


        public RequestGemini()
        {
            _client = new HttpClient();
            //_configuration = configuration;
        }

        public async Task<Root> RequestGeminiAPI(int feliz, int entusi, int relax, string fazendo, string match )
        {
            //string apiKeyGemini = _configuration["ApiKeys:MinhaApiKey"];

            string apiKeyGemini = "";

            string url = $"https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent?key={apiKeyGemini}";

            // Criação do corpo da requisição JSON

            var bodyRequest = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new
                            {
                                text = $"Por favor, indique 9 músicas que {match} do que estou sentindo no momento. Estou me sentindo " +
                                $"{feliz}% feliz, {entusi}% entusiasmado e {relax}% relaxado. Além disso, considere que estou {fazendo}." +
                                $" Retorne apenas o nome da música e o artista, sem adicionar outros textos ou informações."
                            }
                        }
                    }
                }
            };

            // Serializando o corpo para JSON
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(bodyRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Root>(jsonResponse);
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar Geimini: {ex.Message}");
                return null;
            }
        }
    }
}