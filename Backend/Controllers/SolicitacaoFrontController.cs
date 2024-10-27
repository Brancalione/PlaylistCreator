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
        private string spotifyToken360;

        public SolicitacaoFrontController(ISharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;
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
            string[] nomeMusicas = new string[9];
            BuscaIdMusicas buscaIdMusic = new BuscaIdMusicas();
            SpotifyPlaylist spotifyPlaylist = new SpotifyPlaylist();
            spotifyUrlPlaylist urlPlaylist = new spotifyUrlPlaylist();
            InsertMusicPlaylist insertMusicPlaylist = new InsertMusicPlaylist();
            RequestChatGptService requestChatGptService = new RequestChatGptService();

            //await requestChatGptService.RequestChatAsync("Tteste");

            nomeMusicas[0] = "Blinding Lights - The Weeknd";
            nomeMusicas[1] = "Levitating - Dua Lipa";
            nomeMusicas[2] = "Shape of You - Ed Sheeran";
            nomeMusicas[3] = "Drivers License - Olivia Rodrigo";
            nomeMusicas[4] = "Bad Guy - Billie Eilish";
            nomeMusicas[5] = "Peaches - Justin Bieber";
            nomeMusicas[6] = "Watermelon Sugar - Harry Styles";
            nomeMusicas[7] = "Stay - The Kid LAROI & Justin Bieber";
            nomeMusicas[8] = "Good 4 U - Olivia Rodrigo";

            // Preenche a variável spotifyToken360 com o token
            GetSpotifyToken();

            if (string.IsNullOrEmpty(spotifyToken360))
            {
                return NotFound("É necessário fazer login no Spotify antes de solicitar a criação da playlist.");
            }

            try
            {
                // Cria playlist
                var playlistResponse = await spotifyPlaylist.CreatePlaylistAsync(spotifyToken360);
                idPlaylist = playlistResponse.id;
                urlPlaylist = playlistResponse.external_urls;

                // Busca e insere música na playlist
                for (int i = 0; i < nomeMusicas.Length; i++)
                {
                    string uriMusic = await buscaIdMusic.GetIdMusicAsync(nomeMusicas[i], spotifyToken360);
                    await insertMusicPlaylist.InserirMusicPlaylistAsync(uriMusic, spotifyToken360, i, idPlaylist);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar playlist: {ex.Message}");
            }

            // Retorna o link da playlist para o front
            return Ok($"Playlist criada com ID: {JsonSerializer.Serialize(urlPlaylist.spotify)}");

        }
    }
}
