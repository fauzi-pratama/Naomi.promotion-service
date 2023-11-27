namespace Naomi.promotion_service.Models.Response
{
    public class ReportPromoResponse
    {
        public string? CompanyCode { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int TotalPromoUse { get; set; }
        public List<ReportPromoDetailResponse>? Detail { get; set; }
    }

    public class ReportPromoDetailResponse
    {
        public string? Name { get; set; }
        public int PromoUse { get; set; }
        public double PromoUsePercentage { get; set; }
    }
}
