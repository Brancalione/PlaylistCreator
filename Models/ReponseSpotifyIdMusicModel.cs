
namespace ServiceWeb.Models
{
    public class Track
    {
        public string uri { get; set; }
    }

    public class Items
    {
        public Track[] items { get; set; }
    }

    public class Tracks
    {
        public Items tracks { get; set; }
    }
}
