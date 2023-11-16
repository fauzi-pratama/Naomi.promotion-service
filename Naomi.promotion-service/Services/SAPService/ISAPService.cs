
using Naomi.promotion_service.Models.Message.Consume;

namespace Naomi.promotion_service.Services.SAPService
{
    public interface ISAPService
    {
        Task<(bool, string)> HandleMessageCompany(SiteMessage message);
        Task<(bool, string)> HandleMessageSite(SiteMessage message);
        Task<(bool, string)> HandleMessageZone(SiteMessage message);
        Task<(bool, string)> HandleMessageMop(MopMessage message);
    }
}
