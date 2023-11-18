
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.FindPromoService;

namespace Naomi.promotion_service.Controllers.RestApi
{
    [Route("/")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly ILogger<FindController> _logger;
        private readonly IFindPromoService _findPromoService;

        public FindController(ILogger<FindController> logger, IFindPromoService findPromoService)
        {
            _logger = logger;
            _findPromoService = findPromoService;
        }

        [HttpPost("find_promo")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoResponse>>>> FindPromo([FromBody] FindPromoRequest findPromoRequest)
        {
            (List<FindPromoResponse> data, string message, bool cek) = await _findPromoService.FindPromo(findPromoRequest);

            ServiceResponse<List<FindPromoResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? data : new List<FindPromoResponse>(),
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
