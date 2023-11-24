
using Newtonsoft.Json;
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
        public async Task<ActionResult<ServiceResponse<List<EnginePromoResponse>>>> GetDataEnginePromoAsync(EnginePromoRequest enginePromoRequest)
        {
            string dataJson = JsonConvert.SerializeObject(enginePromoRequest);
            _logger.LogInformation($"get_data_engine_promo called with company : {enginePromoRequest.CompanyCode}, datajson : {dataJson}");

            (List<EnginePromoResponse> data, string message, bool cek) = await _engineService.GetDataEnginePromoAsync(enginePromoRequest);

            ServiceResponse<List<EnginePromoResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? data : new List<EnginePromoResponse>(),
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
            {
                _logger.LogInformation($"get_data_engine_promo success with company : {enginePromoRequest.CompanyCode}, message : {message}");
                return Ok(serviceResponse);
            }
            else
            {
                _logger.LogError($"get_data_engine_promo failed with company : {enginePromoRequest.CompanyCode}, message : {message}");
                return NotFound(serviceResponse);
            }
        }

        [HttpGet("refresh_workflow")]
        public ActionResult<ServiceResponse<string>> RefreshWorkflow()
        {
            _logger.LogInformation($"refresh_workflow called");

            (string message, bool cek) = _engineService.RefreshWorkflow();

            ServiceResponse<string> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? "SUCCESS REFRESH WORKFLOW" : "-",
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
            {
                _logger.LogInformation($"refresh_workflow success, message : {message}");
                return Ok(serviceResponse);
            }
            else
            {
                _logger.LogError($"refresh_workflow failed, message : {message}");
                return NotFound(serviceResponse);
            }
        }
    }
}
