
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Services.SoftBookingService
{
    public interface ISoftBookingService
    {
        Task<(List<string>, string, bool)> CekPromoAvailAsync(List<PromoRuleCekAvailRequest> listPromo, string companyCode);

        Task<(bool, string)> PromoRollBackBeforeCommitAsync(PromoWorkflow promoWorkflow, PromoTrans promoTrans);
    }
}
