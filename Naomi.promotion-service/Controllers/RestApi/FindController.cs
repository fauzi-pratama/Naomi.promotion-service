
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.FindPromoService;

namespace Naomi.promotion_service.Controllers.RestApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindController : ControllerBase
    {
        private readonly IFindPromoService _findPromoService;

        public FindController(IFindPromoService findPromoService)
        {
            _findPromoService = findPromoService;
        }

        [HttpPost("find_promo")]
        public async Task<ActionResult<ServiceResponse<List<FindPromoResponse>>>> FindPromo([FromBody] FindPromoRequest findPromoRequest)
    }
}
