using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;
using ServiceWeb.Services;
using System.Threading.Tasks;




namespace ServiceWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoFrontController : ControllerBase
    {

        [HttpPost]// Post padrão
        public async Task<IActionResult> ProcessaFront([FromBody] FormFrontModel respostasForm) // passar no post os parâmetros
        {
            respostasForm.Resposta1 = "teste";

            var spotifyAuth = new SpotifyAuth();

            try
            {
                string clientId = "";
                string clientSecret = "";

                ResponseSpotifyAuthModel tokenResponse = await spotifyAuth.GetTokenAsync(clientId, clientSecret);
                respostasForm.Resposta1 = tokenResponse.access_token;
                respostasForm.Resposta2 = tokenResponse.token_type;
                respostasForm.Resposta3 = $"Expires In: {tokenResponse.expires_in}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }

            return Ok(respostasForm);
        }
    }
}
