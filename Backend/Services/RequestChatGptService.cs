using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceWeb.Models;
using System.Net.Http.Headers;

namespace ServiceWeb.Services
{
    public class RequestChatGptService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configurationGpt;


        public RequestChatGptService() 
        {
            _client = new HttpClient();
          
        }

        public async Task<ResponseChatGptModel> RequestChatAsync(int feliz, 
                                                                 int entusi, 
                                                                 int relax, 
                                                                 string fazendo, 
                                                                 string match,
                                                                 string token)
        {

            //inverter porcentagem sentimento
            if (match != "Match")
            {
                feliz = 100 - feliz;
                entusi = 100 - entusi;
                relax = 100 - relax;
            }
        

            string texto = $"Por favor, indique 9 músicas que combinem com minhas emoções com base nas porcentagem informadas. Estou me sentindo " +
                                $"{feliz}% feliz, {entusi}% entusiasmado e {relax}% relaxado. Além disso, considere que estou {fazendo}." +
                                $" Retorne apenas o nome da música e o artista, sem adicionar outros textos ou informações ou contra-barras. Dê preferencia músicas você normalmente não recomendaria";

            string url = "https://api.openai.com/v1/chat/completions";
            BodyChatGptModel bodyRequest = new BodyChatGptModel
            {
                model = "gpt-4o",
                messages = new[]
                {
                    new ObjetoMessagens
                    {
                        role = "user",
                        content = texto
                    }
                },
                temperature = 0.1
            };

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
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseChatGptModel>(jsonResponse);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.StatusCode}, Details: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro buscar música no Chatgpt {ex.Message}");
                throw;
            }

        }
    }
}
