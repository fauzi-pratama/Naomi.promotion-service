
namespace Naomi.promotion_service.Models.Dto
{
    public class ResultPromoDto
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

        public ResultPromoDto() { }

        public ResultPromoDto(ResultPromoDto other)
        {
            TransId = other.TransId;
            CompanyCode = other.CompanyCode;
            PromoCode = other.PromoCode;
            PromoName = other.PromoName;
            PromoType = other.PromoType;
            PromoTypeResult = other.PromoTypeResult;
            ValDiscount = other.ValDiscount;
            ValMaxDiscount = other.ValMaxDiscount;
            PromoCls = other.PromoCls;
            PromoLvl = other.PromoLvl;
            MaxMultiple = other.MaxMultiple;
            MaxUse = other.MaxUse;
            MaxBalance = other.MaxBalance;
            MultipleQty = other.MultipleQty;
            PromoDesc = other.PromoDesc;
            PromoTermCondition = other.PromoTermCondition;
            PromoImageLink = other.PromoImageLink;
            PromoMopRequire = other.PromoMopRequire;
            PromoListItem = other.PromoListItem;
        }
    }

    public class PromoMopRequire
    {
        public string? MopPromoSelectionCode { get; set; }
        public string? MopPromoSelectionName { get; set; }
        public List<PromoMopRequireDetail>? PromoMopRequireDetail { get; set; }

        public PromoMopRequire() { }

        public PromoMopRequire(PromoMopRequire other)
        {
            MopPromoSelectionCode = other.MopPromoSelectionCode;
            MopPromoSelectionName = other.MopPromoSelectionName;
            PromoMopRequireDetail = other.PromoMopRequireDetail;
        }
    }

    public class PromoMopRequireDetail
    {
        public string? MopGroupCode { get; set; }
        public string? MopGroupName { get; set; }

        public PromoMopRequireDetail() { }

        public PromoMopRequireDetail(PromoMopRequireDetail other)
        {
            MopGroupCode = other.MopGroupCode;
            MopGroupName = other.MopGroupName;
        }
    }

    public class PromoListItem
    {
        public int LinePromo { get; set; }
        public decimal? TotalBefore { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalAfter { get; set; }
        public int? Rounding { get; set; }
        public List<PromoListItemDetail>? PromoListItemDetail { get; set; }

        public PromoListItem() { }

        public PromoListItem(PromoListItem other)
        {
            LinePromo = other.LinePromo;
            TotalBefore = other.TotalBefore;
            TotalDiscount = other.TotalDiscount;
            TotalAfter = other.TotalAfter;
            Rounding = other.Rounding;
            PromoListItemDetail = other.PromoListItemDetail;
        }
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

    public class ItemGroupResultPerPromo
    {
        public string? SkuCode { get; set; }
        public string? Value { get; set; }
        public string? MaxValue { get; set; }
    }
}
