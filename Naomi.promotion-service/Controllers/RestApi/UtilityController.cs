
using Microsoft.AspNetCore.Mvc;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Testing;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.WorkflowPromoService;

namespace Naomi.promotion_service.Controllers.RestApi
{
    [Route("/")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly DataDbContext _dataDbContext;
        private readonly IWorkflowService _workflowService;
        private readonly IPromoSetupService _promoSetupService;

        public UtilityController(DataDbContext dataDbContext, IWorkflowService workflowService, IPromoSetupService promoSetupService)
        {
            _dataDbContext = dataDbContext;
            _workflowService = workflowService;
            _promoSetupService = promoSetupService;
        }

        [HttpGet("reset_data")]
        public async Task<IActionResult> ResetData()
        {
            _dataDbContext.PromoWorkflow.RemoveRange(_dataDbContext.PromoWorkflow);
            _dataDbContext.PromoMasterZone.RemoveRange(_dataDbContext.PromoMasterZone);
            _dataDbContext.PromoMasterSite.RemoveRange(_dataDbContext.PromoMasterSite);

            _dataDbContext.SaveChanges();

            DbTesting dbTesting = new();

            _dataDbContext.PromoWorkflow.AddRange(dbTesting.GetListPromoWorkFlow());
            _dataDbContext.PromoMasterZone.AddRange(dbTesting.GetListPromoMasterZone());
            _dataDbContext.PromoMasterSite.AddRange(dbTesting.GetListPromoMasterSite());

            _dataDbContext.SaveChanges();

            _promoSetupService.RefreshWorkflow(await _workflowService.GetWorkflow());

            return Ok();
        }
    }
}
