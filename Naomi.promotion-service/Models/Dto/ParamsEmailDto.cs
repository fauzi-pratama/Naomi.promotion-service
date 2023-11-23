namespace Naomi.promotion_service.Models.Dto
{
    public class ParamsEmailDto
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<string>? UserReceive { get; set; }
    }
}
