
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Services.OtpPromoService
{
    public interface IOtpService
    {
        Task<(bool, string)> GetOtpAsync(PromoOtpRequest promoOtpRequest);
        Task<(bool, string)> ConfirmOtpAsync(ParamsConfirmOtpDto paramsConfirmOtpDto);
    }
}
