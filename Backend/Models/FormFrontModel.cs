using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class FormFrontModel
    {
        [Required]//Obrigatorio
        public int Resposta1 { get; set; }

        [Required]
        public int Resposta2 { get; set; }

        [Required]
        public int Resposta3 { get; set; }

        [Required]
        public string Resposta4 { get; set; }

        [Required]
        public string Resposta5 { get; set; }
    }
}
