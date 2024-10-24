﻿using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;
using ServiceWeb.Services;
using System.Text;

namespace ServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyCallbackController : ControllerBase
    {
        // Este é o método que receberá o callback
        [HttpGet("callback")]
        public async Task<IActionResult> ReceiveCallback([FromQuery] Dictionary<string, string> queryParams)
        {
            string spotifyToken360;
            string code_response = "";
            spotifycallback spotifyAuthUser = new spotifycallback();

            try
            {
                //Pega token do callback spotify
                foreach (var param in queryParams)
                {
                    code_response = param.Value;
                }

                //Faz token para pegar token de autenticação
                ResponseSpotifyAuthUserModel tokenResponse = await spotifyAuthUser.GetTokenUserAsync(code_response);
                spotifyToken360 = tokenResponse.access_token;

                //HTML para fechar a aba do browser de autenticacao do spotify
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
