
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Services.ReportPromoService
{
    public interface IReportPromoService
    {
        Task<(List<ReportPromoTransactionResponse>, string)> GetReportPromoAsync(string? companyCode, string? startDate, string? endDate, bool enteraiment = false,
            List<string>? listPromo = null);
        Task<(ReportPromoUsageResponse?, string)> GetReportPromoUsageAsync(string? companyCode, string startDate, string endDate);
        Task<(ReportPromoResponse?, string)> GetReportPromoTypeAsync(string? companyCode, string startDate, string endDate, string? zoneName = null);
        Task<(ReportPromoResponse?, string)> GetReportPromoTypeDetailAsync(string? companyCode, string startDate, string endDate, string? promoTypeName = null);
        Task<(ReportPromoUsageEntertaimentResponse?, string)> GetReportPromoEntertaimetUsageAsync(string? companyCode, string? nip, string? startDate, string endDate);
    }
}
