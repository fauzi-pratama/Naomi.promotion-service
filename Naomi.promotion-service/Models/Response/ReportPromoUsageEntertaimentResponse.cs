namespace Naomi.promotion_service.Models.Response
{
    public class ReportPromoUsageEntertaimentResponse
    {
        public string? CompanyCode { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public List<ReportPromoUsageEntertaimentResponseDetail>? ReportDetail { get; set; }
    }

    public class ReportPromoUsageEntertaimentResponseDetail
    {
        public string? PromoMonth { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? PromoQuota { get; set; }
        public decimal? PromoUse { get; set; }
        public decimal? PromoAvaible { get; set; }
    }
}
