﻿using Newtonsoft.Json;
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

        public async Task InserirMusicPlaylistAsync(string idMusica, string token, int l_i)
        {
            string idMusicaEscaped = Uri.EscapeDataString($"spotify:track:{idMusica}");// Remove : da URL e insere o formato correto %2
            string url = $"https://api.spotify.com/v1/playlists/6eyhElSTRfDn2DwMTRILbL/tracks?position={l_i}&uris={idMusicaEscaped}";


            BodySpotifyModel bodyRequest = new BodySpotifyModel();
            bodyRequest.uris = new string[1];
            bodyRequest.uris[0] = $"spotify:track:{idMusica}";
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
                    // Adicionando mais detalhes no erro
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error: {response.StatusCode}, Details: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção adequadamente
                Console.WriteLine($"Erro ao inserir a música na playlist: {ex.Message}");
                throw; // Relança a exceção se necessário
            }
        }
    }
}
