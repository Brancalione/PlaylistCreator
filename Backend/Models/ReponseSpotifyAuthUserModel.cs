using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class ResponseSpotifyAuthUserModel
    {
        [Required]//Obrigatorio
        public string access_token { get; set; }

        [Required]
        public string token_type { get; set; }

        [Required]
        public int expires_in { get; set; }

        [Required]
        public string refresh_token { get; set; }

        [Required]
        public string scope { get; set; }
    }
}
