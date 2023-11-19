namespace Naomi.promotion_service.Models.Dto
{
    public class ResultFindPromoWithoutEngineDto
    {
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoRedeemCode { get; set; }
        public string? PromoName { get; set; }
        public string? PromoType { get; set; }
        public string? PromoTypeResult { get; set; }
        public string? ValDiscount { get; set; }
        public string? ValMaxDiscount { get; set; }
        public int? PromoCls { get; set; }
        public string? PromoDesc { get; set; }
        public string? PromoTermCondition { get; set; }
        public string? PromoImageLink { get; set; }
        public List<ResultFindPromoWithoutEngineDetailDto>? ResultDetail { get; set; }
    }

    public class ResultFindPromoWithoutEngineDetailDto
    {
        public string? SkuCode { get; set; }
        public string? PromoValue { get; set; }
        public string? PromoValueMax { get; set; }
        public string? LinkConnection { get; set; }
    }
}
