using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class ResonseCreatePlaylistModel
    {
        [Required]//Id da playlist criada por API
        public string id { get; set; }
    }

}
