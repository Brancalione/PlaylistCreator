using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class ResponseSpotifyAuthModel
    {
        [Required]//Obrigatorio
        public string access_token { get; set; }

        [Required]
        public string token_type { get; set; }

        [Required]
        public int expires_in { get; set; }
    }
}
