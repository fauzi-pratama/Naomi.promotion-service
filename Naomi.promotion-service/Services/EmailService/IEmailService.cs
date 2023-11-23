
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Message.Consume;

namespace Naomi.promotion_service.Services.EmailService
{
    public interface IEmailService
    {
        (bool, string) SendEmail(ParamsEmailDto paramsEmailDto);
        Task<(bool, string)> SyncEmailUserPromo(EmailUserMessage request);
    }
}
