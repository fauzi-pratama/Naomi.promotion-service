
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.FindPromoService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Route("/v1/")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly ILogger<FindController> _logger;
        private readonly IFindPromoService _findPromoService;
        private readonly IMapper _mapper;

        public FindController(ILogger<FindController> logger, IFindPromoService findPromoService, IMapper mapper)
        {
            _logger = logger;
            _findPromoService = findPromoService;
            _mapper = mapper;
        }

        [HttpPost("find_promo")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoResponse>>>> FindPromoAsync([FromBody] FindPromoRequest findPromoRequest)
        {
            (List<FindPromoResponse> data, string message, bool cek) = await _findPromoService.FindPromoAsync(findPromoRequest);

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

        [HttpPost("find_promo_show")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoShowResponse>>>> FindPromoShowAsync([FromBody] FindPromoShowRequest findPromoShowRequest)
        {
            ParamsFindPromoWithoutEngineDto paramsFindPromoWithoutEngineDto = _mapper.Map<ParamsFindPromoWithoutEngineDto>(findPromoShowRequest);
            (List<ResultFindPromoWithoutEngineDto>? data, string message, bool cek) = await _findPromoService.FindPromoWithoutEngineAsync(paramsFindPromoWithoutEngineDto, true);

            ServiceResponse<List<FindPromoShowResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? _mapper.Map<List<FindPromoShowResponse>>(data) : new List<FindPromoShowResponse>(),
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

        [HttpPost("find_promo_redeem")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoRedeemResponse>>>> FindPromoRedeemAsync([FromBody] FindPromoRedeemRequest findPromoRedeemRequest)
        {
            ParamsFindPromoWithoutEngineDto paramsFindPromoWithoutEngineDto = _mapper.Map<ParamsFindPromoWithoutEngineDto>(findPromoRedeemRequest);
            (List<ResultFindPromoWithoutEngineDto>? data, string message, bool cek) = await _findPromoService.FindPromoWithoutEngineAsync(paramsFindPromoWithoutEngineDto, false);

            ServiceResponse<List<FindPromoRedeemResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? _mapper.Map<List<FindPromoRedeemResponse>>(data) : new List<FindPromoRedeemResponse>(),
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
