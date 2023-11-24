
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Services.EngineService
{
    public interface IEngineService
    {
        (string, bool) RefreshWorkflow();
        Task<(List<EnginePromoResponse>, string, bool)> GetDataEnginePromoAsync(EnginePromoRequest enginePromoRequest);
    }
}
