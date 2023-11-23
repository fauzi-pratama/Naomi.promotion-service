
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.OtpPromoService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Route("/v1/")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;
        private readonly ILogger<OtpController> _logger;

        public OtpController(ILogger<OtpController> logger, IOtpService otpService)
        {
            _logger = logger;
            _otpService = otpService;
        }

        [HttpPost("get_promo_otp")]
        public async Task<ActionResult<ServiceResponse<string>>> GetPromoOtp(PromoOtpRequest promoOtpRequest)
        {
            (bool cekGetOtp, string msgGetOtp) = await _otpService.GetOtp(promoOtpRequest);

            if (!cekGetOtp)
            {
                _logger.LogError($"Failed Get Otp {promoOtpRequest.Nip} : {msgGetOtp} ");

                return NotFound(new ServiceResponse<string>
                {
                    Data = null,
                    Message = msgGetOtp,
                    Success = false
                });
            }

            ServiceResponse<string> serviceResponse = new()
            {
                Data = "Success Get Otp",
            };

            return Ok(serviceResponse);
        }
    }
}
