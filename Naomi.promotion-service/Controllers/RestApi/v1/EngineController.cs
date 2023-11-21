
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.EngineService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Route("/v1/")]
    [ApiController]
    public class EngineController : ControllerBase
    {
        private readonly IEngineService _engineService;
        private readonly ILogger<EngineController> _logger;

        public EngineController(IEngineService engineService, ILogger<EngineController> logger)
        {
            _engineService = engineService;
            _logger = logger;
        }

        [HttpPost("get_data_engine_promo")]
        public async Task<ActionResult<ServiceResponse<List<EnginePromoResponse>>>> GetDataEnginePromo(EnginePromoRequest enginePromoRequest)
        {
            (List<EnginePromoResponse> data, string message, bool cek) = await _engineService.GetDataEnginePromo(enginePromoRequest);

            ServiceResponse<List<EnginePromoResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? data : new List<EnginePromoResponse>(),
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
                return Ok(serviceResponse);
            else
            {
                _logger.LogError(message);

                return NotFound(serviceResponse);
            }
        }

        [HttpGet("refresh_workflow")]
        public ActionResult<ServiceResponse<string>> RefreshWorkflow()
        {
            (string message, bool cek) = _engineService.RefreshWorkflow();

            ServiceResponse<string> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? "SUCCESS REFRESH WORKFLOW" : "-",
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
                return Ok(serviceResponse);
            else
            {
                _logger.LogError(message);

                return NotFound(serviceResponse);
            }
        }
    }
}
