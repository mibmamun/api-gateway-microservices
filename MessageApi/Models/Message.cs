namespace MessageApi.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
