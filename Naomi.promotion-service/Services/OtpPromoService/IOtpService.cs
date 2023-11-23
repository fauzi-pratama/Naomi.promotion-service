
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Services.OtpPromoService
{
    public interface IOtpService
    {
        Task<(bool, string)> GetOtp(PromoOtpRequest promoOtpRequest);
        Task<(bool, string)> ConfirmOtp(ParamsConfirmOtpDto paramsConfirmOtpDto);
    }
}
