
using DotNetCore.CAP;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Message.Consume;
using Naomi.promotion_service.Services.SAPService;

namespace Naomi.promotion_service.Controllers.MessageHandler
{
    public class SapMessageController : ControllerBase
    {
        private readonly ILogger<SapMessageController> _logger;
        private readonly ISAPService _sapService;

        public SapMessageController(ILogger<SapMessageController> logger, ISAPService sapService)
        {
            _logger = logger;
            _sapService = sapService;
        }

        [NonAction]
        [CapSubscribe("site")]
        public async Task HandleSiteMessageAsync(ServiceMessage message)
        {
            if (message == null || message.SyncData == null)
            {
                _logger.LogError("Failed Consume Site Message is null");
                return;
            }

            var data = JsonConvert.DeserializeObject<dynamic>(message.SyncData.ToString()!);

            SiteMessage? dataCompanySite =
                JsonConvert.DeserializeObject<SiteMessage>(JsonConvert.SerializeObject(data));

            (bool cekStatusCompany, string messageStatusCompany) = await _sapService.HandleMessageCompanyAsync(dataCompanySite);
            (bool cekStatusSite, string messageStatusSite) = await _sapService.HandleMessageSiteAsync(dataCompanySite);
            (bool cekStatusZone, string messageStatusZone) = await _sapService.HandleMessageZoneAsync(dataCompanySite);

            if (!cekStatusCompany)
                _logger.LogError($"Failed Consume Company {dataCompanySite.CompanyCode} : {messageStatusCompany} ");
            else
                _logger.LogInformation($"Success Consume Company {dataCompanySite.CompanyCode} : {messageStatusCompany} ");

            if (!cekStatusSite)
                _logger.LogError($"Failed Consume Site {dataCompanySite.SiteCode} : {messageStatusSite} ");
            else
                _logger.LogInformation($"Success Consume Site {dataCompanySite.SiteCode} : {messageStatusSite} ");

            if (!cekStatusZone)
                _logger.LogError($"Failed Consume Zone {dataCompanySite.PricingZone} : {messageStatusZone} ");
            else
                _logger.LogInformation($"Success Consume Zone {dataCompanySite.PricingZone} : {messageStatusZone} ");
        }

        [NonAction]
        [CapSubscribe("mop")]
        public async Task HandleMopMessageAsync(ServiceMessage message)
        {
            if (message == null || message.SyncData == null)
            {
                _logger.LogError("Failed Consume Mop Message is null");
                return;
            }

            var data = JsonConvert.DeserializeObject<dynamic>(message.SyncData.ToString()!);

            MopMessage? dataMop =
                JsonConvert.DeserializeObject<MopMessage>(JsonConvert.SerializeObject(data));

            (bool cekStatusMop, string messageStatusMop) = await _sapService.HandleMessageMopAsync(dataMop);

            if (!cekStatusMop)
                _logger.LogError($"Failed Consume Mop {dataMop.MopCode} : {messageStatusMop} ");
            else
                _logger.LogInformation($"Success Consume Mop {dataMop.MopCode} : {messageStatusMop} ");

            await Task.CompletedTask;
        }
    }
}
