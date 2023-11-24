
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Configurations;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Message.Consume;

namespace Naomi.promotion_service.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly DataDbContext _dataDbContext;
        private readonly IOptions<AppConfig> _appConfig;

        public EmailService(DataDbContext dataDbContext, IOptions<AppConfig> appConfig)
        {
            _dataDbContext = dataDbContext;
            _appConfig = appConfig;
        }

        public async Task<(bool, string)> SyncEmailUserPromoAsync(EmailUserMessage request)
        {
            if (request is null || request.Nip is null || request.Email is null)
                return (false, "Params is not Valid !!");

            PromoMasterUserEmail? promoUserEmail = await _dataDbContext.PromoMasterUserEmail.
                FirstOrDefaultAsync(q => q.Nip!.ToUpper() == request.Nip!.ToUpper());

            if (promoUserEmail is not null)
            {
                promoUserEmail.Email = request.Email;
                promoUserEmail.ActiveFlag = (bool)request.ActiveFlag!;
            }
            else
            {
                promoUserEmail = new()
                {
                    Id = Guid.NewGuid(),
                    Nip = request.Nip!.ToUpper(),
                    Email = request.Email,
                    ActiveFlag = request.ActiveFlag,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.UtcNow
                };

                _dataDbContext.PromoMasterUserEmail.Add(promoUserEmail);
            }

            await _dataDbContext.SaveChangesAsync();

            return (true, "Success");
        }

        public (bool, string) SendEmail(ParamsEmailDto paramsEmailDto)
        {
            if (paramsEmailDto == null || string.IsNullOrEmpty(paramsEmailDto.Body) || string.IsNullOrEmpty(paramsEmailDto.Subject) ||
                paramsEmailDto.UserReceive == null || paramsEmailDto.UserReceive.Count < 1)
                return (false, "Params is not Valid !!");

            if (string.IsNullOrEmpty(_appConfig.Value.EmailDomain) || string.IsNullOrEmpty(_appConfig.Value.EmailHost) || _appConfig.Value.EmailPort == 0)
                return (false, "Email Config is not Valid !!");

            try
            {
                MailMessage mailMessage = new()
                {
                    From = new(_appConfig.Value.EmailDomain),
                    Subject = paramsEmailDto.Subject,
                    Body = paramsEmailDto.Body,
                    IsBodyHtml = true,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.Default,
                };

                foreach (var userEmailLoop in paramsEmailDto.UserReceive)
                {
                    mailMessage.To.Add(userEmailLoop);
                }

                SmtpClient mailCLient = new(_appConfig.Value.EmailHost)
                {
                    Port = Convert.ToInt32(_appConfig.Value.EmailPort)
                };

                mailCLient.Send(mailMessage);
                mailCLient.Dispose();

                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
