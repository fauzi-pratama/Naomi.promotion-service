
using DotNetCore.CAP;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Message.Consume;
using Naomi.promotion_service.Services.OtpPromoService;
using Naomi.promotion_service.Services.EmailService;

namespace Naomi.promotion_service.Controllers.MessageHandler
{
    public class EmailMessageController : ControllerBase
    {
        private readonly ILogger<EmailMessageController> _logger;
        private readonly IEmailService _emailService;

        public EmailMessageController(ILogger<EmailMessageController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [NonAction]
        [CapSubscribe("promo_email_user")]
        public async Task PromoEmailSubscriberAsync(ServiceMessage message)
        {
            if (message == null || message.SyncData == null)
            {
                _logger.LogError("Failed Consume Email Message is null");
                return;
            }

            var data = JsonConvert.DeserializeObject<dynamic>(message.SyncData.ToString()!);

            EmailUserMessage? dataEmailUser =
                JsonConvert.DeserializeObject<EmailUserMessage>(JsonConvert.SerializeObject(data));

            (bool cekStatusEmailUser, string messageStatusEmailUser) = await _emailService.SyncEmailUserPromoAsync(dataEmailUser);

            if (!cekStatusEmailUser)
                _logger.LogError($"Failed Consume Email User {dataEmailUser.Nip} : {messageStatusEmailUser} ");
            else
                _logger.LogInformation($"Success Consume Email User {dataEmailUser.Nip} : {messageStatusEmailUser} ");
        }
    }
}
