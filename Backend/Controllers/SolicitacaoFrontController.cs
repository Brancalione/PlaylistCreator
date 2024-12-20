﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;
using ServiceWeb.Services;
using System.Text.Json;

namespace ServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoFrontController : ControllerBase
    {
        private readonly ISharedDataService _sharedDataService;
        private readonly IConfiguration _configurationGpt;
        private string spotifyToken360;

        public SolicitacaoFrontController(ISharedDataService sharedDataService, IConfiguration configurationGpt)
        {
            _sharedDataService = sharedDataService;
            _configurationGpt = configurationGpt;
        }

        // Método privado para obter o token sem retorno
        private void GetSpotifyToken()
        {
            // Acessa o token armazenado no serviço
            spotifyToken360 = _sharedDataService.SpotifyToken;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessaFront([FromBody] FormFrontModel respostasForm)
        {
            string idPlaylist;
            string musicas;
            string[] nomeMusicas = new string[9];
            TextSeparator separator = new TextSeparator();
            BuscaIdMusicas buscaIdMusic = new BuscaIdMusicas();
            SpotifyPlaylist spotifyPlaylist = new SpotifyPlaylist();
            spotifyUrlPlaylist urlPlaylist = new spotifyUrlPlaylist();
            InsertMusicPlaylist insertMusicPlaylist = new InsertMusicPlaylist();
            RequestChatGptService requestChatGptService = new RequestChatGptService();

            // Preenche a variável spotifyToken360 com o token
            GetSpotifyToken();

            if (string.IsNullOrEmpty(spotifyToken360))
            {
                return NotFound("É necessário fazer login no Spotify antes de solicitar a criação da playlist.");
            }

            try
            {
                string apiKeyGpt = _configurationGpt["ApiKeys:apiKeyGpt"];
                var textoReponse = await requestChatGptService.RequestChatAsync(respostasForm.Resposta1,
                                                                                respostasForm.Resposta2,
                                                                                respostasForm.Resposta3,
                                                                                respostasForm.Resposta4,
                                                                                respostasForm.Resposta5,
                                                                                apiKeyGpt);
                musicas = textoReponse.Choices[0].Message.Content;
                nomeMusicas = await separator.SeparateTextAsync(musicas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar playlist: {ex.Message}");
            }

            try
            {
                // Cria playlist
                var playlistResponse = await spotifyPlaylist.CreatePlaylistAsync(spotifyToken360, respostasForm.Resposta4);
                idPlaylist = playlistResponse.id;
                urlPlaylist = playlistResponse.external_urls;

                // Busca e insere música na playlist
                for (int i = 0; i < 9; i++)
                {
                    string uriMusic = await buscaIdMusic.GetIdMusicAsync(nomeMusicas[i], spotifyToken360);
                    if (uriMusic != ""){
                        await insertMusicPlaylist.InserirMusicPlaylistAsync(uriMusic, spotifyToken360, i, idPlaylist);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar playlist: {ex.Message}");
            }

            // Retorna o link da playlist para o front
            return Ok(new { url = idPlaylist });
        }
    }
}
