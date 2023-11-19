
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Services.FindPromoService
{
    public interface IFindPromoService
    {
        Task<(List<FindPromoResponse>, string, bool)> FindPromo(FindPromoRequest findPromoRequest);
        Task<(List<ResultFindPromoWithoutEngineDto>?, string, bool)> FindPromoWithoutEngine(ParamsFindPromoWithoutEngineDto findPromoWithoutEngineRequest, bool promoShow);
    }
}
