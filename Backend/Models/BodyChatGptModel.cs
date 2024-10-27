namespace ServiceWeb.Models
{
    public class BodyChatGptModel
    {
        public string model { get; set; }
        public ObjetoMessagens[] messages { get; set; }
        public double temperature { get; set; }
    }

    public class ObjetoMessagens
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
