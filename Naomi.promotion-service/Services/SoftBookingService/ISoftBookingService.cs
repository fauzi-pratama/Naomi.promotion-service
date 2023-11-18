
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Services.SoftBookingService
{
    public interface ISoftBookingService
    {
        Task<(List<string>, string, bool)> CekPromoAvail(List<PromoRuleCekAvailRequest> listPromo, string companyCode);

        Task<(bool, string)> PromoRollBackBeforeCommit(PromoWorkflow promoWorkflow, PromoTrans promoTrans);
    }
}
