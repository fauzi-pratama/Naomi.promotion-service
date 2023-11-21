
namespace Naomi.promotion_service.Models.Request
{
    public class FindPromoShowRequest
    {
        public string? CompanyCode { get; set; }
        public string? PromotionApp { get; set; }
        public string? SiteCode { get; set; }
    }
}
