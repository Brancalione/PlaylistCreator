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
            string spotifyToken360;
            SpotifyAuth spotifyAuth = new SpotifyAuth();

            try
            {
                //Pega token acesso spotify
                ResponseSpotifyAuthModel tokenResponse = await spotifyAuth.GetTokenAsync();
                spotifyToken360 = tokenResponse.access_token;
 
            }
            catch (Exception ex)
            {
                //exceção geral, erros de API do chat e Sportify caem aqui
                Console.WriteLine(ex.Message);
                return NotFound();
            }

            //Aqui vai retornar o link da playlist para o front
            return Ok(respostasForm);
        }
    }
}
