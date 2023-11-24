
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
        public async Task<ActionResult<ServiceResponse<string>>> GetPromoOtpAsync(PromoOtpRequest promoOtpRequest)
        {
            _logger.LogInformation($"get_promo_otp called with nip : {promoOtpRequest.Nip}");

            (bool cekGetOtp, string msgGetOtp) = await _otpService.GetOtpAsync(promoOtpRequest);

            if (!cekGetOtp)
            {
                _logger.LogError($"get_promo_otp with nip : {promoOtpRequest.Nip} Failed Get Otp : {msgGetOtp} ");

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

            _logger.LogInformation($"get_promo_otp with Success Get Otp {promoOtpRequest.Nip} : {msgGetOtp} ");
            return Ok(serviceResponse);
        }
    }
}
