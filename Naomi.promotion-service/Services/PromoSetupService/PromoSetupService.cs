
using RulesEngine.Models;
using Naomi.promotion_service.Configurations;

namespace Naomi.promotion_service.Services.PromoSetupService
{
    public class PromoSetupService : IPromoSetupService
    {
        private RulesEngine.RulesEngine? _rulesEngine;

        public (bool, string) RefreshWorkflow(string[] workflowRules)
        {
            try
            {
                ReSettings reSettings = new()
                {
                    CustomActions = new()
                {
                    {"ResultPromo", () => new PromoEngineResult()}
                }
                };

                _rulesEngine = new RulesEngine.RulesEngine(workflowRules, reSettings);

                return (true, "Success");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
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
