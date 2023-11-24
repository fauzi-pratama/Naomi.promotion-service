
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.FindPromoService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Authorize]
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
            string dataJson = JsonConvert.SerializeObject(findPromoRequest);
            _logger.LogInformation($"find_promo called with company : {findPromoRequest.CompanyCode}, transid : {findPromoRequest.TransId}, datajson : {dataJson} ");

            (List<FindPromoResponse> data, string message, bool cek) = await _findPromoService.FindPromoAsync(findPromoRequest);

            ServiceResponse<List<FindPromoResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? data : new List<FindPromoResponse>(),
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
            {
                _logger.LogInformation($"find_promo success with company : {findPromoRequest.CompanyCode}, transid : {findPromoRequest.TransId}, message : {message}");
                return Ok(serviceResponse);
            }
            else
            {
                _logger.LogError($"find_promo failed with company : {findPromoRequest.CompanyCode}, transid : {findPromoRequest.TransId}, message : {message}");
                return NotFound(serviceResponse);
            }
        }

        [HttpPost("find_promo_show")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoShowResponse>>>> FindPromoShowAsync([FromBody] FindPromoShowRequest findPromoShowRequest)
        {
            string dataJson = JsonConvert.SerializeObject(findPromoShowRequest);
            _logger.LogInformation($"find_promo_show called with company : {findPromoShowRequest.CompanyCode}, datajson : {dataJson}");

            ParamsFindPromoWithoutEngineDto paramsFindPromoWithoutEngineDto = _mapper.Map<ParamsFindPromoWithoutEngineDto>(findPromoShowRequest);
            (List<ResultFindPromoWithoutEngineDto>? data, string message, bool cek) = await _findPromoService.FindPromoWithoutEngineAsync(paramsFindPromoWithoutEngineDto, true);

            ServiceResponse<List<FindPromoShowResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? _mapper.Map<List<FindPromoShowResponse>>(data) : new List<FindPromoShowResponse>(),
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
            {
                _logger.LogInformation($"find_promo_show success with company : {findPromoShowRequest.CompanyCode}, message : {message}");
                return Ok(serviceResponse);
            }
            else
            {
                _logger.LogError($"find_promo_show failed with company : {findPromoShowRequest.CompanyCode}, message : {message}");
                return NotFound(serviceResponse);
            }
        }

        [HttpPost("find_promo_redeem")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoRedeemResponse>>>> FindPromoRedeemAsync([FromBody] FindPromoRedeemRequest findPromoRedeemRequest)
        {
            string dataJson = JsonConvert.SerializeObject(findPromoRedeemRequest);
            _logger.LogInformation($"find_promo_redeem called with company : {findPromoRedeemRequest.CompanyCode}, datajson : {dataJson}");

            ParamsFindPromoWithoutEngineDto paramsFindPromoWithoutEngineDto = _mapper.Map<ParamsFindPromoWithoutEngineDto>(findPromoRedeemRequest);
            (List<ResultFindPromoWithoutEngineDto>? data, string message, bool cek) = await _findPromoService.FindPromoWithoutEngineAsync(paramsFindPromoWithoutEngineDto, false);

            ServiceResponse<List<FindPromoRedeemResponse>> serviceResponse = new()
            {
                Data = message == "SUCCESS" ? _mapper.Map<List<FindPromoRedeemResponse>>(data) : new List<FindPromoRedeemResponse>(),
                Success = message == "SUCCESS",
                Message = message
            };

            if (cek)
            {
                _logger.LogInformation($"find_promo_redeem success with company : {findPromoRedeemRequest.CompanyCode}, message : {message}");
                return Ok(serviceResponse);
            }
            else
            {
                _logger.LogError($"find_promo_redeem failed with company : {findPromoRedeemRequest.CompanyCode}, message : {message}");
                return NotFound(serviceResponse);
            }
        }

    }
}
