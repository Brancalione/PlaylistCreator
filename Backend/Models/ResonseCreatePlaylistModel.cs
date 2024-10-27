using System.ComponentModel.DataAnnotations;

namespace ServiceWeb.Models
{
    public class ResonseCreatePlaylistModel
    {
        [Required]//Id da playlist criada por API
        public string id { get; set; }

        public spotifyUrlPlaylist external_urls { get; set; } //Url da playlist
    }

    public class spotifyUrlPlaylist
    {
        public string spotify { get; set; }
    }

}
