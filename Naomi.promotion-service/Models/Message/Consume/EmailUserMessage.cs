namespace Naomi.promotion_service.Models.Message.Consume
{
    public class EmailUserMessage
    {
        public string? Nip { get; set; }
        public string? Email { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
