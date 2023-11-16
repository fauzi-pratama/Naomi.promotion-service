
using DotNetCore.CAP;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Naomi.promotion_service.Controllers.MessageHandler
{
    public class SapMessageController : ControllerBase
    {
        private readonly ILogger<SapMessageController> _logger;

        public SapMessageController(ILogger<SapMessageController> logger)
        {
            _logger = logger;
        }

        [NonAction]
        [CapSubscribe("site")]
        public void SiteMessage(JsonElement jsonElement)
        {
            _logger.LogDebug("Receive message site : {jsonElement}", jsonElement);
        }
    }
}
