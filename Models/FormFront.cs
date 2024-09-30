using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class FormFrontModel
    {
        [Required]//Obrigatorio
        public string Resposta1 { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Errou -5")]
        public string Resposta2 { get; set; }

        [Required]
        public string Resposta3 { get; set; }

        [Required]
        public string Resposta4 { get; set; }

        [Required]
        public string Resposta5 { get; set; }
    }
}
