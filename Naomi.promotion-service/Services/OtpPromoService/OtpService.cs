
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Contexts;

namespace Naomi.promotion_service.Services.OtpPromoService
{
    public class OtpService : IOtpService
    {
        private readonly DataDbContext _dataDbContext;

        public OtpService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<(bool, string)> ConfirmOtp(string companyCode, string nip, string otp)
        {
            bool isCheckOtp = await _dataDbContext.PromoOtp
                .AnyAsync(q => q.CompanyCode!.ToUpper() == companyCode.ToUpper() && q.Nip!.ToUpper() == nip!.ToUpper() 
                    && q.Code == otp && q.ExpDate > DateTime.UtcNow && !q.IsUse && q.ActiveFlag);

            if (!isCheckOtp)
                return (false, "Otp is not Valid !!");

            return (true, "Success");
        }
    }
}
