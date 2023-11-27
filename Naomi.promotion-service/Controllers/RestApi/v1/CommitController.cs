
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.CommitService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Route("/v1/")]
    [ApiController]
    public class CommitController : ControllerBase
    {
        private readonly ICommitService _commitService;
        private readonly ILogger<CommitController> _logger;

        public CommitController(ICommitService commitService, ILogger<CommitController> logger)
        {
            _commitService = commitService;
            _logger = logger;
        }

        [HttpPost("commit_promo")]
        public async Task<ActionResult<ServiceResponse<string>>> CommitPromoAsync([FromBody] CommitRequest commitRequest)
        {
            _logger.LogInformation($"Called commit_promo with params : {JsonConvert.SerializeObject(commitRequest)}");

            ServiceResponse<string> serviceResponse = new();

            (bool status, string message) = await _commitService.CommitPromoAsync(commitRequest);

            if (status)
            {
                serviceResponse.Data = message;
                serviceResponse.Message = "Success Commit Promo";
                serviceResponse.Success = true;

                _logger.LogInformation($"Success commit_promo with params : {JsonConvert.SerializeObject(commitRequest)}");

                return Ok(serviceResponse);
            }
            else
            {
                serviceResponse.Message = message;
                serviceResponse.Success = false;

                _logger.LogInformation($"Failed commit_promo with params : {JsonConvert.SerializeObject(commitRequest)}");

                return NotFound(serviceResponse);
            }
        }

        [HttpPost("cancel_promo")]
        public async Task<ActionResult<ServiceResponse<string>>> CancelPromoAsync([FromBody] CancelPromoRequest cancelPromoRequest)
        {
            _logger.LogInformation($"Called cancel_promo with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

            ServiceResponse<string> serviceResponse = new();

            (bool status, string message) = await _commitService.CancelPromoAsync(cancelPromoRequest);

            if (status)
            {
                serviceResponse.Data = message;
                serviceResponse.Message = "Success Cancel Promo";
                serviceResponse.Success = true;

                _logger.LogInformation($"Success cancel_promo with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

                return Ok(serviceResponse);
            }
            else
            {
                serviceResponse.Message = message;
                serviceResponse.Success = false;

                _logger.LogInformation($"Failed cancel_promo with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

                return NotFound(serviceResponse);
            }
        }

        [HttpPost("cancel_promo_commited")]
        public async Task<ActionResult<ServiceResponse<string>>> CancelPromoCommitedAsync([FromBody] CancelPromoRequest cancelPromoRequest)
        {
            _logger.LogInformation($"Called cancel_promo_commited with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

            ServiceResponse<string> serviceResponse = new();

            (bool status, string message) = await _commitService.CancelPromoAsync(cancelPromoRequest, true);

            if (status)
            {
                serviceResponse.Data = message;
                serviceResponse.Message = "Success Cancel Promo";
                serviceResponse.Success = true;

                _logger.LogInformation($"Success cancel_promo_commited with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

                return Ok(serviceResponse);
            }
            else
            {
                serviceResponse.Message = message;
                serviceResponse.Success = false;

                _logger.LogInformation($"Failed cancel_promo_commited with params : {JsonConvert.SerializeObject(cancelPromoRequest)}");

                return NotFound(serviceResponse);
            }
        }
    }
}
