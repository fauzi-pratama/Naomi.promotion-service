
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.WorkflowPromoService;

namespace Naomi.promotion_service.Services.EngineService
{
    public class EngineService : IEngineService
    {
        private readonly DataDbContext _dataDbContext;
        private readonly IPromoSetupService _promoSetupService;
        private readonly IWorkflowService _workflowService;

        public EngineService(DataDbContext dataDbContext, IPromoSetupService promoSetupService, IWorkflowService workflowService)
        {
            _dataDbContext = dataDbContext;
            _promoSetupService = promoSetupService;
            _workflowService = workflowService;
        }

        public async Task<(List<EnginePromoResponse>, string, bool)> GetDataEnginePromoAsync(EnginePromoRequest enginePromoRequest)
        {
            try
            {
                FindPromoRequest findPromoRequest = new();
                List<EnginePromoResponse> listEngineResultCekResponse = new();
                List<string> listCompanyWorkflowEngine = _promoSetupService.GetCompanyWorkflow();

                if (!string.IsNullOrEmpty(enginePromoRequest.CompanyCode))
                {
                    listCompanyWorkflowEngine = listCompanyWorkflowEngine.Where(q => q == enginePromoRequest.CompanyCode).ToList();

                    if (listCompanyWorkflowEngine.Count < 1)
                        return (new List<EnginePromoResponse>(), "Company not Registered", false);
                }

                foreach (var loopCompanyWorkflow in listCompanyWorkflowEngine)
                {
                    (List<RulesEngine.Models.RuleResultTree> listResultEngine, string message, bool cek ) = 
                        await _promoSetupService.GetPromoAsync(loopCompanyWorkflow, findPromoRequest, true);

                    foreach (var loopResultEngine in listResultEngine)
                    {
                        PromoRule? promoRule = _dataDbContext.PromoRule.FirstOrDefault(q => q.Code == loopResultEngine.Rule.RuleName);

                        List<VariableEngine> listVarEngine = new();

                        foreach (var loopLocalParamsEngine in loopResultEngine.Rule.LocalParams)
                        {
                            VariableEngine varEngine = new()
                            {
                                Name = loopLocalParamsEngine.Name,
                                Expression = loopLocalParamsEngine.Expression
                            };

                            listVarEngine.Add(varEngine);
                        }

                        EnginePromoResponse engineResultCekResponse = new()
                        {
                            CompanyCode = loopCompanyWorkflow,
                            PromoCode = loopResultEngine.Rule.RuleName,
                            PromoName = promoRule!.Name,
                            Status = loopResultEngine.IsSuccess,
                            ExpressionEngine = loopResultEngine.Rule.Expression,
                            VariableEngine = listVarEngine,
                        };

                        listEngineResultCekResponse.Add(engineResultCekResponse);
                    }

                    if (!string.IsNullOrEmpty(enginePromoRequest.PromoName) && !string.IsNullOrEmpty(enginePromoRequest.PromoCode))
                    {
                        listEngineResultCekResponse = listEngineResultCekResponse.Where(q => q.PromoName!.Contains(enginePromoRequest.PromoName) && q.PromoCode!.Contains(enginePromoRequest.PromoCode)).ToList();

                        if (listEngineResultCekResponse.Count < 1)
                            return (new List<EnginePromoResponse>(), "No Have Promo Registered !!", false);
                    }
                    else if (!string.IsNullOrEmpty(enginePromoRequest.PromoCode))
                    {
                        listEngineResultCekResponse = listEngineResultCekResponse.Where(q => q.PromoCode!.Contains(enginePromoRequest.PromoCode)).ToList();

                        if (listEngineResultCekResponse.Count < 1)
                            return (new List<EnginePromoResponse>(), "No Have Promo Registered !!", false);
                    }
                    else if (!string.IsNullOrEmpty(enginePromoRequest.PromoName))
                    {
                        listEngineResultCekResponse = listEngineResultCekResponse.Where(q => q.PromoName!.Contains(enginePromoRequest.PromoName)).ToList();

                        if (listEngineResultCekResponse.Count < 1)
                            return (new List<EnginePromoResponse>(), "No Have Promo Registered !!", false);
                    }
                }

                return (listEngineResultCekResponse, "SUCCESS", true);
            }
            catch (Exception ex)
            {
                return (new List<EnginePromoResponse>(), ex.Message, false);
            }
        }

        public (string, bool) RefreshWorkflow()
        {
            try
            {
                string[] listWorkflow = _workflowService.GetWorkflow();

                if (listWorkflow is null || listWorkflow.Length < 1)
                    return ("Data Workflow is Nothing !!", false);

                (bool cek, string message) = _promoSetupService.RefreshWorkflow(listWorkflow);

                if (!cek)
                    return (message, true);

                return ("SUCCESS", true);
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
