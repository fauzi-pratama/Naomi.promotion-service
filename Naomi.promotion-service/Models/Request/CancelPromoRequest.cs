namespace Naomi.promotion_service.Models.Request
{
    public class CancelPromoRequest
    {
        public string? CompanyCode { get; set; }
        public string? TransId { get; set; }
    }
}
