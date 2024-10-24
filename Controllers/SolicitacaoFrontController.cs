using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;
using ServiceWeb.Services;


namespace ServiceWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoFrontController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> ProcessaFront([FromBody] FormFrontModel respostasForm)
        {
            string[] nomeMusicas = new string[9];
            string spotifyToken360;
            string idPlaylist;
            SpotifyAuth spotifyAuth = new SpotifyAuth();
            BuscaIdMusicas buscaIdMusic = new BuscaIdMusicas();
            string id_playlist = "7oMBCzoz1adHxopW60GeB4";

            nomeMusicas[0] = "Blinding Lights - The Weeknd";
            nomeMusicas[1] = "Levitating - Dua Lipa";
            nomeMusicas[2] = "Shape of You - Ed Sheeran";
            nomeMusicas[3] = "Drivers License - Olivia Rodrigo";
            nomeMusicas[4] = "Bad Guy - Billie Eilish";
            nomeMusicas[5] = "Peaches - Justin Bieber";
            nomeMusicas[6] = "Watermelon Sugar - Harry Styles";
            nomeMusicas[7] = "Stay - The Kid LAROI & Justin Bieber";
            nomeMusicas[8] = "Good 4 U - Olivia Rodrigo";

            try
            {
                //Pega token
                ResponseSpotifyAuthModel tokenResponse = await spotifyAuth.GetTokenAsync();
                spotifyToken360 = tokenResponse.access_token;
                
                //Cria playlist
                SpotifyPlaylist spotifyPlaylist = new SpotifyPlaylist();
                var playlistResponse = await spotifyPlaylist.CreatePlaylistAsync(spotifyToken360);
                idPlaylist = playlistResponse.id;

                //Busca e insere musica na playlist
                InsertMusicPlaylist insertMusicPlaylist = new InsertMusicPlaylist();
                for (int i = 0; i < nomeMusicas.Length; i++)
                {
                    string uriMusic = await buscaIdMusic.GetIdMusicAsync(nomeMusicas[i], spotifyToken360);
                    await insertMusicPlaylist.InserirMusicPlaylistAsync(uriMusic, spotifyToken360, i , idPlaylist);
                }
            }
            catch (Exception ex)
            {
                // Exceção geral, erros de API do chat e Spotify caem aqui
                return NotFound();
            }

            // Aqui vai retornar o link da playlist para o front
            return Ok();
        }
    }
}