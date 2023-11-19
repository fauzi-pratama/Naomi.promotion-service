
using RulesEngine.Models;

namespace Naomi.promotion_service.Services.PromoSetupService
{
    public interface IPromoSetupService
    {
        (bool, string) RefreshWorkflow(string[] workflowRules);
        List<string> GetCompanyWorkflow();
        Task<List<RuleResultTree>> GetPromo(string workflowPromo, object findDataPromo, bool getDetail = false);
    }
}
