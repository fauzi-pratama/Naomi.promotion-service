
namespace Naomi.promotion_service.Models.Response
{
    public class FindPromoShowResponse
    {
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public string? PromoType { get; set; }
        public string? PromoTypeResult { get; set; }
        public string? ValDiscount { get; set; }
        public string? ValMaxDiscount { get; set; }
        public int? PromoCls { get; set; }
        public string? PromoDesc { get; set; }
        public string? PromoTermCondition { get; set; }
        public string? PromoImageLink { get; set; }
        public List<FindPromoShowDetailResponse>? ResultDetail { get; set; }
    }

    public class FindPromoShowDetailResponse
    {
        public string? SkuCode { get; set; }
        public string? PromoValue { get; set; }
        public string? PromoValueMax { get; set; }
        public string? LinkConnection { get; set; }
    }
}
