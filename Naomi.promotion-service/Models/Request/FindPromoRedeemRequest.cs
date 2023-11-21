
namespace Naomi.promotion_service.Models.Request
{
    public class FindPromoRedeemRequest
    {
        public string? CompanyCode { get; set; }
        public string? PromotionApp { get; set; }
        public string? RedeemCode { get; set; }
    }
}
