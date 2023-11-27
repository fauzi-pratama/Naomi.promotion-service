
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.ReportPromoService;

namespace Naomi.promotion_service.Controllers.RestApi.v1
{
    [Route("/v1/")]
    [ApiController]

    public class ReportController : ControllerBase
    {
        private readonly IReportPromoService _reportPromoService;

        public ReportController(IReportPromoService reportPromoService)
        {
            _reportPromoService = reportPromoService;
        }

        [HttpGet("get_report_promo_all")]
        public async Task<IActionResult> GetReportPromoAllAsync(string companyCode, string startDate, string endDate)
        {
            (List<ReportPromoTransactionResponse> responseData, string message) = await _reportPromoService.GetReportPromoAsync(companyCode, startDate, endDate);

            if (message == "Success")
                return Ok(responseData);
            else
                return NotFound(message);
        }

        [HttpGet("get_report_promo_usage")]
        public async Task<ActionResult<ServiceResponse<ReportPromoUsageResponse>>> GetReportPromoUsageAsync(string companyCode, string startDate, string endDate)
        {
            ServiceResponse<ReportPromoUsageResponse> response = new();

            (ReportPromoUsageResponse? responseData, string? message) =
                    await _reportPromoService.GetReportPromoUsageAsync(companyCode, startDate, endDate);

            if (message == "Success" && responseData != null)
            {
                response.Data = responseData;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }

        [HttpGet("get_report_promo_type")]
        public async Task<ActionResult<ServiceResponse<ReportPromoResponse>>> GetReportPromoTypeAsync(string companyCode, string startDate, string endDate)
        {
            ServiceResponse<ReportPromoResponse> response = new();

            (ReportPromoResponse? dataReport, string message) = await _reportPromoService.GetReportPromoTypeAsync(companyCode, startDate, endDate);

            if (message == "Success" && dataReport != null)
            {
                response.Data = dataReport;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }

        [HttpGet("get_report_promo_type_detail")]
        public async Task<ActionResult<ServiceResponse<ReportPromoResponse>>> GetReportPromoTypeDetailAsync(string companyCode, string startDate, string endDate, string promoTypeName)
        {
            ServiceResponse<ReportPromoResponse> response = new();

            (ReportPromoResponse? dataReport, string message) = await _reportPromoService.GetReportPromoTypeDetailAsync(companyCode, startDate, endDate, promoTypeName);

            if (message == "Success" && dataReport != null)
            {
                response.Data = dataReport;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }

        [HttpGet("get_report_promo_zone")]
        public async Task<ActionResult<ServiceResponse<ReportPromoResponse>>> GetReportPromoZoneAsync(string companyCode, string startDate, string endDate)
        {
            ServiceResponse<ReportPromoResponse> response = new();

            (ReportPromoResponse? dataReport, string message) = await _reportPromoService.GetReportPromoTypeDetailAsync(companyCode, startDate, endDate);

            if (message == "Success" && dataReport != null)
            {
                response.Data = dataReport;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }

        [HttpGet("get_report_promo_zone_detail")]
        public async Task<ActionResult<ServiceResponse<ReportPromoResponse>>> GetReportPromoZoneDetailAsync(string companyCode, string startDate, string endDate, string zoneName)
        {
            ServiceResponse<ReportPromoResponse> response = new();

            (ReportPromoResponse? dataReport, string message) = await _reportPromoService.GetReportPromoTypeAsync(companyCode, startDate, endDate, zoneName);

            if (message == "Success" && dataReport != null)
            {
                response.Data = dataReport;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }

        [HttpGet("get_report_promo_usage_entertaiment")]
        public async Task<ActionResult<ServiceResponse<ReportPromoUsageEntertaimentResponse>>> GetReportPromoUsageEntertaimentAsync(string? companyCode, string? nip, string startDate, string endDate)
        {
            ServiceResponse<ReportPromoUsageEntertaimentResponse> response = new();

            (ReportPromoUsageEntertaimentResponse? responseData, string? message) =
                    await _reportPromoService.GetReportPromoEntertaimetUsageAsync(companyCode, nip, startDate, endDate);

            if (message == "Success" && responseData != null)
            {
                response.Data = responseData;

                return Ok(response);
            }
            else
            {
                response.Message = message;
                response.Success = false;
            }

            return NotFound(response);
        }
    }
}
