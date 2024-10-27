using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;
using ServiceWeb.Services;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyCallbackController : ControllerBase
    {
        // Criação de dependências entre controllers
        private readonly ISharedDataService _sharedDataService;
        private readonly SpotifyCallback _spotifyAuthUser; // Injeção de SpotifyCallback

        public SpotifyCallbackController(ISharedDataService sharedDataService, SpotifyCallback spotifyAuthUser)
        {
            _sharedDataService = sharedDataService;
            _spotifyAuthUser = spotifyAuthUser;
        }

        [HttpGet("callback")]
        public async Task<IActionResult> ReceiveCallback([FromQuery] Dictionary<string, string> queryParams)
        {
            string codeResponse = "";

            try
            {
                // Pega o código de resposta do Spotify no callback
                foreach (var param in queryParams)
                {
                    codeResponse = param.Value;
                }

                // Usa o código para obter o token de autenticação
                ResponseSpotifyAuthUserModel tokenResponse = await _spotifyAuthUser.GetTokenUserAsync(codeResponse);
                _sharedDataService.SpotifyToken = tokenResponse.access_token;

                // HTML para fechar a aba do navegador de autenticação do Spotify
                string htmlContent = @"
                    <html>
                    <head><title>Fechando Aba</title></head>
                    <body>
                        <script type='text/javascript'>
                            window.close();
                        </script>
                        <p>Fechando a aba...</p>
                    </body>
                    </html>";

                return Content(htmlContent, "text/html", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar callback: {ex.Message}");
            }
        }
    }
}
