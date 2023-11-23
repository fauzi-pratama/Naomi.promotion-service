
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

        public CommitController(ICommitService commitService)
        {
            _commitService = commitService;
        }

        //[HttpPost("commit_promo")]
        //public async Task<ActionResult<ServiceResponse<string>>> CommitPromo(CommitRequest commitRequest)
        //{
        //    (bool cekCommit, string messageCommit) = await _commitService.CommitPromo(commitRequest);

        //    if (!cekCommit)
        //        return NotFound();

        //    ServiceResponse<string> response = new()
        //    {
        //        Data = "SUCCESS COMMIT PROMO !!",
        //        Message = messageCommit,
        //        Success = true,
        //        Pages = 1,
        //        TotalPages = 1
        //    };
        //}
    }
}
