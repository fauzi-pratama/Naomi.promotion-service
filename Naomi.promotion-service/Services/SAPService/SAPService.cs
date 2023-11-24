
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Message.Consume;

namespace Naomi.promotion_service.Services.SAPService
{
    public class SAPService : ISAPService
    {
        private readonly DataDbContext _dataDbContext;

        public SAPService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<(bool, string)> HandleMessageCompanyAsync(SiteMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.CompanyCode) || string.IsNullOrEmpty(message.CompanyDescription))
                    return (false, "CompanyCode or CompanyDescription is null or empty");

                PromoWorkflow? promoWorkflow =
                    _dataDbContext.PromoWorkflow.FirstOrDefault(q => q.Code!.ToUpper() == message.CompanyCode!.ToUpper());

                if (promoWorkflow != null)
                {
                    promoWorkflow.Name = message.CompanyDescription;
                    promoWorkflow.UpdatedBy = "System";
                    promoWorkflow.UpdatedDate = DateTime.UtcNow;
                }
                else
                {
                    PromoWorkflow promoWorkflowNew = new()
                    {
                        Id = Guid.NewGuid(),
                        Code = message.CompanyCode.ToUpper(),
                        Name = message.CompanyDescription,
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    };

                    //List<PromoWorkflowExpression> dataListExpression =
                    //    _converterManager.GetPromoExpressionApi(promoWorkflowNew);

                    _dataDbContext.PromoWorkflow.Add(promoWorkflowNew);
                    //_dataDbContext.PromoWorkflowExpression.AddRange(dataListExpression);
                }

                await _dataDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "Success");
        }

        public async Task<(bool, string)> HandleMessageSiteAsync(SiteMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.CompanyCode) || string.IsNullOrEmpty(message.SiteCode) || string.IsNullOrEmpty(message.SiteDescription))
                    return (false, "CompanyCode or SiteCode or SiteDescription is null or empty");

                PromoMasterSite? promoMasterSite =
                    _dataDbContext.PromoMasterSite.FirstOrDefault(q => q.CompanyCode!.ToUpper() == message.CompanyCode!.ToUpper() &&
                        q.SiteCode!.ToUpper() == message.SiteCode!.ToUpper());

                if (promoMasterSite != null)
                {
                    promoMasterSite.SiteName = message.SiteDescription!;
                    promoMasterSite.UpdatedBy = "System";
                    promoMasterSite.UpdatedDate = DateTime.UtcNow;
                }
                else
                {
                    PromoMasterSite promoMasterSiteNew = new()
                    {
                        Id = Guid.NewGuid(),
                        CompanyCode = message.CompanyCode!.ToUpper(),
                        SiteCode = message.SiteCode!.ToUpper(),
                        SiteName = message.SiteDescription!,
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    };

                    _dataDbContext.PromoMasterSite.Add(promoMasterSiteNew);
                }

                await _dataDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "Success");
        }

        public async Task<(bool, string)> HandleMessageZoneAsync(SiteMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.CompanyCode) || string.IsNullOrEmpty(message.SiteCode) || string.IsNullOrEmpty(message.PricingZone))
                    return (false, "CompanyCode or SiteCode or PricingZoneCode is null or empty");

                PromoMasterZone? promoMasterZone =
                    _dataDbContext.PromoMasterZone.FirstOrDefault(q => q.CompanyCode!.ToUpper() == message.CompanyCode!.ToUpper() && q.SiteCode!.ToUpper() == message.SiteCode!.ToUpper()
                        && q.PricingZoneCode!.ToUpper() == message.PricingZone!.ToUpper());

                if (promoMasterZone != null)
                {
                    promoMasterZone.PricingZoneName = message.PricingZone!;
                    promoMasterZone.UpdatedBy = "System";
                    promoMasterZone.UpdatedDate = DateTime.UtcNow;
                }
                else
                {
                    PromoMasterZone promoMasterZoneNew = new()
                    {
                        Id = Guid.NewGuid(),
                        CompanyCode = message.CompanyCode.ToUpper(),
                        SiteCode = message.SiteCode.ToUpper(),
                        PricingZoneCode = message.PricingZone.ToUpper(),
                        PricingZoneName = message.PricingZone!,
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    };

                    _dataDbContext.PromoMasterZone.Add(promoMasterZoneNew);
                }

                await _dataDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "Success");
        }

        public async Task<(bool, string)> HandleMessageMopAsync(MopMessage message)
        {
            try
            {
                if (string.IsNullOrEmpty(message.SalesOrganizationCode) || string.IsNullOrEmpty(message.SiteCode) || string.IsNullOrEmpty(message.MopCode) ||
                    string.IsNullOrEmpty(message.MopName))
                    return (false, "SalesOrganizationCode or SiteCode or MopCode or MopName is null or empty");

                PromoMasterMop? promoMasterMop =
                    _dataDbContext.PromoMasterMop.FirstOrDefault(q =>
                        q.CompanyCode!.ToUpper() == message.SalesOrganizationCode!.ToUpper() && q.SiteCode!.ToUpper() == message.SiteCode!.ToUpper() &&
                            q.MopCode!.ToUpper() == message.MopCode!.ToUpper());

                if (promoMasterMop != null)
                {
                    promoMasterMop.MopName = message.MopName;
                    promoMasterMop.UpdatedBy = "System";
                    promoMasterMop.UpdatedDate = DateTime.UtcNow;
                }
                else
                {
                    PromoMasterMop promoMasterMopNew = new()
                    {
                        Id = Guid.NewGuid(),
                        CompanyCode = message.SalesOrganizationCode!.ToUpper(),
                        SiteCode = message.SiteCode!.ToUpper(),
                        MopCode = message.MopCode!.ToUpper(),
                        MopName = message.MopName!,
                        ActiveFlag = true,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    };

                    _dataDbContext.PromoMasterMop.Add(promoMasterMopNew);
                }

                await _dataDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "Success");
        }
    }
}
