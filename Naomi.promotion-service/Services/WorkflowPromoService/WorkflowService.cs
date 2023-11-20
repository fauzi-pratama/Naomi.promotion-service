
using System.Text;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Services.WorkflowPromoService
{
    public class WorkflowService : IWorkflowService
    {
        private readonly DataDbContext _dbContext;

        public WorkflowService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string[] GetWorkflow()
        {
            List<string> promoWorkflowList = new();

            ICollection<PromoWorkflow> listPromoWorkflow;

            //Get Data from Db
            listPromoWorkflow = _dbContext.PromoWorkflow
                .Where(q => q.ActiveFlag)
                .Include(q => q.PromoWorkflowExpression)
                .ToList();

            //Return Out Jika Tidak Terdapat Promo Workflow
            if (listPromoWorkflow.Count < 1)
                return promoWorkflowList.ToArray();

            foreach (var promoWorkflowHeader in listPromoWorkflow)
            {
                //Add Global Params
                List<GlobalParam> listGlobalParams = new();

                if (promoWorkflowHeader.PromoWorkflowExpression?.Count > 0)
                {
                    foreach (var loopGlobalParams in promoWorkflowHeader.PromoWorkflowExpression)
                    {
                        GlobalParam GlobalParams = new()
                        {
                            Name = loopGlobalParams.Code,
                            Expression = loopGlobalParams.Expression
                        };

                        listGlobalParams.Add(GlobalParams);
                    }
                }

                //get data rule table
                ICollection<PromoRule> listPromoRule = _dbContext.PromoRule
                    .Where(q => q.PromoWorkflowId == promoWorkflowHeader.Id
                        && q.ActiveFlag && q.EndDate >= DateTime.UtcNow)
                    .Include(q => q.PromoRuleMop)
                    .Include(q => q.PromoRuleVariable)
                    .Include(q => q.PromoRuleExpression)
                    .Include(q => q.PromoRuleResult)
                    .ToList();

                //Next Loop Jika Promo Rule Tidak di Temukan
                if (listPromoRule.Count < 1)
                    continue;

                //Add Rule 
                List<Rule> listRules = new();

                foreach (var loopRules in listPromoRule)
                {
                    StringBuilder ruleExp = new();
                    int groupLine = 0, countGroupLine = 0, countGroupLineSave = 0;

                    List<LocalParam> listLocalParams = new();

                    //Add Local Params
                    //Add Local Params Variable
                    if (loopRules.PromoRuleVariable?.Count > 0)
                    {
                        foreach (var loopLocalParamsVar in loopRules.PromoRuleVariable)
                        {
                            LocalParam localParamas = new()
                            {
                                Name = loopLocalParamsVar.Code,
                                Expression = loopLocalParamsVar.ParamsExpression
                            };

                            listLocalParams.Add(localParamas);
                        }
                    }

                    //Get Max GroupLine
                    int maxGroupLine = loopRules.PromoRuleExpression!.Max(q => q.GroupLine);

                    //Add Local Params Expresion
                    if (loopRules.PromoRuleExpression?.Count > 0)
                    {
                        foreach (var loopLocalParamsExp in loopRules.PromoRuleExpression.OrderBy(q => q.LineNum))
                        {

                            LocalParam localParamas = new()
                            {
                                Name = loopLocalParamsExp.Code,
                                Expression = loopLocalParamsExp.ParamsExpression
                            };

                            listLocalParams.Add(localParamas);

                            var linkExp = loopLocalParamsExp.LinkExp != null &&
                                            loopLocalParamsExp.LinkExp != "" ? $" {loopLocalParamsExp.LinkExp} " : "";

                            if (loopLocalParamsExp.GroupLine != 0)
                            {
                                if (loopLocalParamsExp.GroupLine > groupLine)
                                {

                                    countGroupLineSave = loopRules.PromoRuleExpression
                                                        .Count(q => q.GroupLine == loopLocalParamsExp.GroupLine);
                                    countGroupLine = loopRules.PromoRuleExpression
                                                        .Count(q => q.GroupLine == loopLocalParamsExp.GroupLine);

                                    if (loopLocalParamsExp.GroupLine == 2 && countGroupLine == countGroupLineSave && countGroupLine != 1)
                                    {
                                        ruleExp.Append("((" + loopLocalParamsExp.Code + linkExp);
                                    }
                                    else if (loopLocalParamsExp.GroupLine == 2 && countGroupLine == countGroupLineSave && maxGroupLine != loopLocalParamsExp.GroupLine && countGroupLine == 1)
                                    {
                                        ruleExp.Append("((" + loopLocalParamsExp.Code + ")" + linkExp);
                                    }
                                    else if (loopLocalParamsExp.GroupLine == 2 && countGroupLine == countGroupLineSave && maxGroupLine == loopLocalParamsExp.GroupLine && countGroupLine == 1)
                                    {
                                        ruleExp.Append("((" + loopLocalParamsExp.Code + "))");
                                    }
                                    else if (loopLocalParamsExp.GroupLine != 2 && maxGroupLine == loopLocalParamsExp.GroupLine && countGroupLine == 1)
                                    {
                                        ruleExp.Append('(' + loopLocalParamsExp.Code + "))");
                                    }
                                    else if (loopLocalParamsExp.GroupLine != 2 && countGroupLine == 1)
                                    {
                                        ruleExp.Append('(' + loopLocalParamsExp.Code + ")" + linkExp);
                                    }
                                    else
                                    {
                                        ruleExp.Append('(' + loopLocalParamsExp.Code + linkExp);
                                    }

                                    groupLine++;
                                    countGroupLine--;
                                }
                                else
                                {

                                    if (maxGroupLine == loopLocalParamsExp.GroupLine && countGroupLine == 1)
                                    {
                                        if (loopRules.Cls == 2 || loopRules.Cls == 3 || loopRules.Cls == 4)
                                            ruleExp.Append(loopLocalParamsExp.Code + ")");
                                        else
                                            ruleExp.Append(loopLocalParamsExp.Code + "))");
                                    }
                                    else if (countGroupLine == 1)
                                    {
                                        ruleExp.Append(loopLocalParamsExp.Code + ")" + linkExp);
                                    }
                                    else
                                    {
                                        ruleExp.Append(loopLocalParamsExp.Code + linkExp);
                                    }

                                    countGroupLine--;
                                }
                            }
                            else
                            {
                                ruleExp.Append(loopLocalParamsExp.Code + linkExp);
                            }
                        }
                    }

                    PromoRule getDataResult = loopRules;
                    getDataResult.PromoRuleVariable = null;
                    getDataResult.PromoRuleExpression = null;

                    Context addContext = new()
                    {
                        DataPromo = getDataResult
                    };

                    OnSuccess addOnsuccess = new()
                    {
                        Name = "ResultPromo",
                        Context = addContext
                    };

                    Actions addAction = new()
                    {
                        OnSuccess = addOnsuccess
                    };

                    Rule rules = new()
                    {
                        Actions = addAction,
                        RuleName = loopRules.Code,
                        SuccessEvent = Convert.ToString(loopRules.Cls) + "#" + Convert.ToString(loopRules.Lvl),
                        LocalParams = listLocalParams,
                        Expression = ruleExp.ToString(),
                    };

                    listRules.Add(rules);
                }

                //Add Workflow 
                RuleWorkflowDto ruleWorkflowDto = new()
                {
                    WorkflowName = promoWorkflowHeader.Code,
                    GlobalParams = listGlobalParams,
                    Rules = listRules
                };

                string convertToString = JsonConvert.SerializeObject(ruleWorkflowDto);
                promoWorkflowList.Add(convertToString);
            }

            return promoWorkflowList.ToArray();
        }
    }
}

