using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceWeb.Models;


namespace ServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoFrontController : ControllerBase
    {
        [HttpPost]// Post padrão
        public IActionResult ProcessaFront([FromBody] FormFrontModel respostasForm)// passar no post os parametros
        {
            respostasForm.Resposta1 = "teste";
            //Recebera 5 resposta do frot
            return Ok(respostasForm);
        }

    }
}

