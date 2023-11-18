namespace Naomi.promotion_service.Services.OtpPromoService
{
    public interface IOtpService
    {
        Task<(bool, string)> ConfirmOtp(string companyCode, string nip, string otp);
    }
}
