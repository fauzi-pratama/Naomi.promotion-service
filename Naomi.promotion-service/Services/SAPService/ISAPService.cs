
using Naomi.promotion_service.Models.Message.Consume;

namespace Naomi.promotion_service.Services.SAPService
{
    public interface ISAPService
    {
        Task<(bool, string)> HandleMessageCompanyAsync(SiteMessage message);
        Task<(bool, string)> HandleMessageSiteAsync(SiteMessage message);
        Task<(bool, string)> HandleMessageZoneAsync(SiteMessage message);
        Task<(bool, string)> HandleMessageMopAsync(MopMessage message);
    }
}
