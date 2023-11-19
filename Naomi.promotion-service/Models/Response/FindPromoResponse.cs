
namespace Naomi.promotion_service.Models.Response
{
    public class FindPromoResponse
    {
        public string? TransId { get; set; }
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public string? PromoType { get; set; }
        public string? PromoTypeResult { get; set; }
        public string? ValDiscount { get; set; }
        public string? ValMaxDiscount { get; set; }
        public bool ValMaxDiscountStatus { get; set; } = false;
        public int? PromoCls { get; set; }
        public int? PromoLvl { get; set; }
        public int? MaxMultiple { get; set; }
        public int? MaxUse { get; set; }
        public decimal? MaxBalance { get; set; }
        public int? MultipleQty { get; set; }
        public string? PromoDesc { get; set; }
        public string? PromoTermCondition { get; set; }
        public string? PromoImageLink { get; set; }
        public PromoMopRequire? PromoMopRequire { get; set; }
        public List<PromoListItem>? PromoListItem { get; set; }
    }

    public class PromoMopRequire
    {
        public string? MopPromoSelectionCode { get; set; }
        public string? MopPromoSelectionName { get; set; }
        public List<PromoMopRequireDetail>? PromoMopRequireDetail { get; set; }
    }

    public class PromoMopRequireDetail
    {
        public string? MopGroupCode { get; set; }
        public string? MopGroupName { get; set; }
        public PromoMopRequireDetail() { }
    }

    public class PromoListItem
    {
        public int LinePromo { get; set; }
        public decimal? TotalBefore { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalAfter { get; set; }
        public int? Rounding { get; set; }
        public List<PromoListItemDetail>? PromoListItemDetail { get; set; }
    }

    public class PromoListItemDetail
    {
        public int LineNo { get; set; }
        public string? SkuCode { get; set; }
        public string? ValDiscount { get; set; }
        public string? ValMaxDiscount { get; set; }
        public decimal? Price { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalAfter { get; set; }
        public PromoListItemDetail() { }
    }
}
