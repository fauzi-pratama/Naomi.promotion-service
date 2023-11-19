using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Models.Testing
{
    public class DbTesting
    {

        #region "promo_workflow"

        private readonly List<PromoWorkflow> listPromoWorkFlow = new()
        {
            new PromoWorkflow()
            {
                Id = Guid.NewGuid(),
                Code = "F100",
                Name = "PT Foods Beverages Indonesia",
                ActiveFlag = true,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow,
                PromoWorkflowExpression = new()
                {
                    new PromoWorkflowExpression
                    {
                        Id = Guid.NewGuid(),
                        Code = "varGlblTotalPrice",
                        Name = "Total Price Item",
                        Expression = "Convert.ToDecimal(paramsPromo.ItemProduct.Sum(y => Convert.ToDecimal(y.price) * Convert.ToDecimal(y.qty)))",
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow,
                    },
                    new PromoWorkflowExpression
                    {
                        Id = Guid.NewGuid(),
                        Code = "varGlblTotalQty",
                        Name = "Total Qty Item",
                        Expression = "Convert.ToDecimal(paramsPromo.ItemProduct.Sum(y => Convert.ToDecimal(y.qty)))",
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow,
                    },
                    new PromoWorkflowExpression
                    {
                        Id = Guid.NewGuid(),
                        Code = "varGlblPrmDate",
                        Name = "Params Date",
                        Expression = "Convert.ToDateTime(paramsPromo.TransDate)",
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow,
                    }
                },
                PromoRule = new()
                {
                    new PromoRule()
                    {
                        Id = Guid.NewGuid(),
                        Code = "PROMO001",
                        Name = "PROMO001_CLASS(CART)_TYPE(AMOUNT)_REQ(AND)_SBOOK(FALSE)_MULTIPLE(FALSE)_RESULT(AND)",
                        Cls = 2,
                        Lvl = 2,
                        ItemType = "ALL",
                        ResultType = "V2",
                        PromoActionType = "AMOUNT",
                        PromoActionValue = "10000",
                        MaxValue = "",
                        MaxMultiple = 1,
                        MaxBalance = 0,
                        MaxUse = 0,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        MultipleQty = 1,
                        ActiveFlag = true,
                        PromoShow = true,
                        PromoDesc = "Desction Promo Cart Amount",
                        PromoImageLink = "",
                        PromoTermCondition = "Terms Condition Promo Cart Amount",
                        RedeemCode = "RDMPROMO001",
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow,
                        PromoRuleSite = new()
                        {
                            new PromoRuleSite
                            {
                                Id = Guid.NewGuid(),
                                Code = "F314",
                                Name = "ST CHATIME EAST COAST SBY",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleSite
                            {
                                Id = Guid.NewGuid(),
                                Code = "F794",
                                Name = "ST CT SPBU KALIABANG",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            }
                        },
                        PromoRuleVariable = new()
                        {
                            new PromoRuleVariable
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 1,
                                Code = "varLclStartDate",
                                Name = "Start Date",
                                ParamsExpression = "Convert.ToDateTime(\"2023-02-01 00:00:00\")",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleVariable
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 2,
                                Code = "varLclEndDate",
                                Name = "End Date",
                                ParamsExpression = "Convert.ToDateTime(\"2023-02-01 23:59:00\")",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            }
                        },
                        PromoRuleExpression = new()
                        {
                            new PromoRuleExpression
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 1,
                                GroupLine = 1,
                                Code = "cekDateStart",
                                Name = "Cek Date Start",
                                ParamsExpression = "varGlblPrmDate >= varLclStartDate",
                                LinkExp = "AND",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleExpression
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 2,
                                GroupLine = 1,
                                Code = "cekDateEnd",
                                Name = "Cek Date End",
                                ParamsExpression = "varLclEndDate >= varGlblPrmDate",
                                LinkExp = "AND",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleExpression
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 3,
                                GroupLine = 1,
                                Code = "cekSite",
                                Name = "Cek Site",
                                ParamsExpression = "new string[]{\"F314\",\"F794\"}.Contains(paramsPromo.SiteCode)",
                                LinkExp = "AND",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleExpression
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 4,
                                GroupLine = 1,
                                Code = "cekZone",
                                Name = "Cek Zone",
                                ParamsExpression = "new string[]{\"Z1\",\"Z2\"}.Contains(paramsPromo.ZoneCode)",
                                LinkExp = "AND",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            },
                            new PromoRuleExpression
                            {
                                Id = Guid.NewGuid(),
                                LineNum = 5,
                                GroupLine = 1,
                                Code = "cekTotalPriceStart",
                                Name = "Cek Total Price Start",
                                ParamsExpression = "varGlblTotalPrice >= 10000",
                                LinkExp = "",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            }
                        },
                        PromoRuleApps = new()
                        {
                            new PromoRuleApps()
                            {
                                Id = Guid.NewGuid(),
                                Code = "ODI",
                                Name = "ODI",
                                ActiveFlag = true,
                                CreatedBy = "System",
                                CreatedDate = DateTime.UtcNow
                            }
                        }
                    }
                }
            }
        };

        public List<PromoWorkflow> GetListPromoWorkFlow()
        {
            return listPromoWorkFlow;
        }

        #endregion

        #region "promo_master_zone"

        private readonly List<PromoMasterZone> listPromoMasterZone = new()
        {
            new PromoMasterZone()
            {
                Id = Guid.NewGuid(),
                CompanyCode = "F100",
                SiteCode = "F314",
                PricingZoneCode = "Z1",
                PricingZoneName = "Z1",
                ActiveFlag = true,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            },
            new PromoMasterZone()
            {
                Id = Guid.NewGuid(),
                CompanyCode = "F100",
                SiteCode = "F794",
                PricingZoneCode = "Z2",
                PricingZoneName = "Z2",
                ActiveFlag = true,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            },
        };

        public List<PromoMasterZone> GetListPromoMasterZone()
        {
            return listPromoMasterZone;
        }

        #endregion

        #region "promo_master_site"

        private readonly List<PromoMasterSite> listPromoMasterSite = new()
        {
            new PromoMasterSite()
            {
                Id = Guid.NewGuid(),
                CompanyCode = "F100",
                SiteCode = "F314",
                SiteName = "ST CHATIME EAST COAST SBY",
                ActiveFlag = true,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            },
            new PromoMasterSite()
            {
                Id = Guid.NewGuid(),
                CompanyCode = "F100",
                SiteCode = "F794",
                SiteName = "ST CT SPBU KALIABANG",
                ActiveFlag = true,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            }
        };

        public List<PromoMasterSite> GetListPromoMasterSite()
        {
            return listPromoMasterSite;
        }   

        #endregion
    }
}
