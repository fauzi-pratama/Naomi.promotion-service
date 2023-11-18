
using Naomi.promotion_service.Configurations;
using RulesEngine.Actions;
using RulesEngine.Models;

namespace Naomi.promotion_service.Services.PromoSetupService
{
    public class PromoSetupService : IPromoSetupService
    {
        private RulesEngine.RulesEngine? _rulesEngine;

        public bool RefreshWorkflow(string[] workflowRules)
        {
            var reSettings = new ReSettings
            {
                CustomActions = new Dictionary<string, Func<ActionBase>>{
                                          {"ResultPromo", () => new PromoEngineResult()}
                                      }
            };

            _rulesEngine = new RulesEngine.RulesEngine(workflowRules, reSettings);

            return true;
        }

        public async Task<List<RuleResultTree>> GetPromo(string workflowPromo, object findDataPromo, bool getDetail = false)
        {
            RuleParameter paramsPromo = new("paramsPromo", findDataPromo);
            List<RuleResultTree> resultList = await _rulesEngine!.ExecuteAllRulesAsync(workflowPromo, paramsPromo);

            if (!getDetail)
                resultList = resultList.Where(q => q.IsSuccess).ToList();

            return resultList;
        }

        public List<string> GetCompanyWorkflow()
        {
            return _rulesEngine!.GetAllRegisteredWorkflowNames();
        }
    }
}
