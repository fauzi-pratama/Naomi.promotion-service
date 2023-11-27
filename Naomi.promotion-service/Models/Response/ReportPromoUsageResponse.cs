
namespace Naomi.promotion_service.Models.Response
{
    public class ReportPromoUsageResponse
    {
        public string? CompanyCode { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public List<ReportPromoUsageResponseDetail>? ReportDetail { get; set; }
    }

    public class ReportPromoUsageResponseDetail
    {
        public string? PromoType { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? PromoQuota { get; set; }
        public string? PromoUse { get; set; }
        public string? PromoAvaible { get; set; }
    }
}
