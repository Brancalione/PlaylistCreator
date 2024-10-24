using Microsoft.AspNetCore.Mvc;

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
            try
            {
                // Aqui você pode logar ou trabalhar com os dados recebidos
                // 'queryParams' é um dicionário que contém todos os parâmetros de consulta
                Console.WriteLine("Recebendo callback do Spotify:");
                foreach (var param in queryParams)
                {
                    Console.WriteLine($"{param.Key}: {param.Value}");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Se houver algum erro, retorne um código de erro apropriado
                return StatusCode(500, $"Erro ao processar callback: {ex.Message}");
            }
        }
    }
}
