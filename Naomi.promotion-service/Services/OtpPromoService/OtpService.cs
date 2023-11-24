
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Configurations;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Services.EmailService;

namespace Naomi.promotion_service.Services.OtpPromoService
{
    public class OtpService : IOtpService
    {
        private readonly DataDbContext _dataDbContext;
        private readonly IOptions<AppConfig> _appConfig;
        private readonly IEmailService _emailService;

        public OtpService(DataDbContext dataDbContext, IOptions<AppConfig> appConfig, IEmailService emailService)
        {
            _dataDbContext = dataDbContext;
            _appConfig = appConfig;
            _emailService = emailService;
        }

        public async Task<(bool, string)> GetOtpAsync(PromoOtpRequest promoOtpRequest)
        {
            try
            {
                PromoOtp? promoOtp = _dataDbContext.PromoOtp.OrderByDescending(q => q.ExpDate)
                .FirstOrDefault(q => q.CompanyCode == promoOtpRequest.CompanyCode && q.ExpDate > DateTime.UtcNow
                                    && !q.IsUse && q.ActiveFlag);

                PromoMasterUserEmail? promoUserEmail = await _dataDbContext.PromoMasterUserEmail
                    .FirstOrDefaultAsync(q => q.Nip!.ToUpper() == promoOtpRequest.Nip!.ToUpper() && q.ActiveFlag);

                string otpCode;

                if (promoUserEmail is null || string.IsNullOrEmpty(promoUserEmail.Email))
                    return (false, "Master Email Not Found");

                if (promoOtp is null || string.IsNullOrEmpty(promoOtp.Code))
                {
                    otpCode = GetOtp(6);

                    if (otpCode is null)
                        return (false, "Failed Get Random Otp");

                    PromoOtp newPromoOtp = new()
                    {
                        Id = Guid.NewGuid(),
                        CompanyCode = promoOtpRequest.CompanyCode,
                        Nip = promoOtpRequest.Nip,
                        Code = otpCode,
                        ExpDate = DateTime.UtcNow.AddMinutes(5),
                        IsUse = false,
                        ActiveFlag = true,
                        CreatedDate = DateTime.UtcNow
                    };

                    _dataDbContext.PromoOtp.Add(newPromoOtp);
                }
                else
                {
                    otpCode = promoOtp.Code;
                    promoOtp.ExpDate = DateTime.UtcNow.AddMinutes(5);
                }

                await _dataDbContext.SaveChangesAsync();

                string emailTemplate;
                string path = Path.Combine(Environment.CurrentDirectory, @"Template", "Email.html");

                using (StreamReader sr = new(path))
                {
                    emailTemplate = await sr.ReadToEndAsync();
                    emailTemplate = emailTemplate.Replace("{varOtpCode}", otpCode);
                }

                ParamsEmailDto paramsEmailDto = new()
                {
                    Subject = "Promo Otp",
                    Body = emailTemplate,
                    UserReceive = new List<string>() { promoUserEmail.Email }
                };

                (bool cekEmail, string msgEmail) = _emailService.SendEmail(paramsEmailDto);

                if (!cekEmail)
                    return (true, $"Failed Send Email Otp, {msgEmail}");

                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> ConfirmOtpAsync(ParamsConfirmOtpDto paramsConfirmOtpDto)
        {
            try
            {
                if (paramsConfirmOtpDto == null || string.IsNullOrEmpty(paramsConfirmOtpDto.CompanyCode) || string.IsNullOrEmpty(paramsConfirmOtpDto.Nip) ||
                string.IsNullOrEmpty(paramsConfirmOtpDto.Otp))
                    return (false, "Params is not Valid !!");

                bool isCheckOtp = await _dataDbContext.PromoOtp
                    .AnyAsync(q => q.CompanyCode!.ToUpper() == paramsConfirmOtpDto.CompanyCode!.ToUpper() && q.Nip!.ToUpper() == paramsConfirmOtpDto.Nip!.ToUpper()
                        && q.Code == paramsConfirmOtpDto.Otp && q.ExpDate > DateTime.UtcNow && !q.IsUse && q.ActiveFlag);

                if (!isCheckOtp)
                    return (false, "Otp is not Valid !!");

                return (true, "Success");
            } catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private string GetOtp(int length)
        {
            string templateRandomOtp = _appConfig.Value.TemplateRandomOtp!;
            Random rdm = new();
            StringBuilder keyRandom = new();

            for (int i = 0; i < length; i++)
            {
                int a = rdm.Next(templateRandomOtp.Length);
                keyRandom.Append(templateRandomOtp.ElementAt(a));
            }

            return keyRandom.ToString();
        }
    }
}
