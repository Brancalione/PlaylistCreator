using ServiceWeb.Models;
using System.Text.Json.Serialization;

namespace ServiceWeb.Models
{
    public class Root
    {
        [JsonPropertyName("candidates")]
        public Candidate[] Candidates { get; set; }
    }

    public class Candidate
    {
        [JsonPropertyName("content")]
        public Content Content { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("parts")]
        public Part[] Parts { get; set; }
    }

    public class Part
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
//var text = root.Candidates[0].Content.Parts[0].Text;
