using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Services.CommitService
{
    public interface ICommitService 
    {
        Task<(bool, string)> CommitPromoAsync(CommitRequest commitRequest);
        Task<(bool, string)> CancelPromoAsync(CancelPromoRequest cancelPromoRequest, bool commited = false);
    }
}
