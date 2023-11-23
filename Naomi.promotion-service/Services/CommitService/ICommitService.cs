using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Services.CommitService
{
    public interface ICommitService 
    {
        Task<(bool, string)> CommitPromo(CommitRequest commitRequest);
        Task<(bool, string)> CancelPromo(CancelPromoRequest cancelPromoRequest, bool commited = false);
    }
}
