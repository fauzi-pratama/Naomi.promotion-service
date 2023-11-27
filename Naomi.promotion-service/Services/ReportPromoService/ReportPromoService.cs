
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Services.ReportPromoService
{
    public class ReportPromoService : IReportPromoService
    {
        private readonly DataDbContext _dataDbContext;

        public ReportPromoService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<(List<ReportPromoTransactionResponse>, string)> GetReportPromoAsync(string? companyCode, string? startDate, string? endDate, bool enteraiment = false,
            List<string>? listPromo = null)
        {
            List<ReportPromoTransactionResponse> dataResponse = new();
            List<PromoRule> listPromoRule;

            try
            {
                PromoWorkflow? promoWorkflow = await _dataDbContext.PromoWorkflow
                    .Include(q => q.PromoRule!.Where(q => q.ActiveFlag))
                    .FirstOrDefaultAsync(q => q.Code == companyCode);

                ICollection<PromoMaster>? listPromoMaster = await _dataDbContext.PromoMaster.ToListAsync();
                ICollection<PromoTransDetail> listPromoTransDetail = await _dataDbContext.PromoTransDetail.ToListAsync();
                ICollection<PromoTrans> listPromoTrans = await _dataDbContext.PromoTrans.Include(q => q.PromoTransDetail).ToListAsync();

                if (promoWorkflow == null || promoWorkflow.PromoRule == null)
                    return (dataResponse, $"Promo Workflow or Promo Rule is Null With Company : {companyCode} !!");

                if (listPromoMaster.Count < 1 || listPromoTransDetail.Count < 1 || listPromoTrans.Count < 1)
                    return (dataResponse, $"Promo Master or PromoTransDetail or PromoTrans is Null With Company : {companyCode} !!");

                if (!string.IsNullOrEmpty(companyCode))
                {
                    if (enteraiment)
                    {
                        if (listPromo == null)
                            return (dataResponse, "Promo List Entertaiment is Null !!");

                        listPromoRule = promoWorkflow.PromoRule.Where(q => listPromo!.Contains(q.Code!)).ToList();
                    }
                    else
                    {
                        listPromoRule = promoWorkflow.PromoRule.Where(q => q.ActiveFlag && q.PromoWorkflowId == promoWorkflow.Id).ToList();
                    }
                }
                else
                {
                    if (enteraiment)
                    {
                        if (listPromo == null)
                            return (dataResponse, "Promo List Entertaiment is Null !!");

                        listPromoRule = promoWorkflow.PromoRule.Where(q => q.ActiveFlag && listPromo!.Contains(q.Code!)).ToList();

                    }
                    else
                    {
                        listPromoRule = promoWorkflow.PromoRule.Where(q => q.ActiveFlag).ToList();
                    }
                }

                if (listPromoRule.Count < 1)
                    return (dataResponse, "Promo Rule is Null !!");

                foreach (var loopPromoRule in listPromoRule)
                {
                    PromoMaster? promoMaster = !string.IsNullOrEmpty(companyCode) ?
                        listPromoMaster.FirstOrDefault(q => q.PromoCode == loopPromoRule.Code && q.CompanyCode == companyCode) :
                        listPromoMaster.FirstOrDefault(q => q.PromoCode == loopPromoRule.Code);

                    List<Guid?> listPromoTransDetails = listPromoTransDetail
                        .Where(q => q.PromoCode == loopPromoRule.Code && q.ActiveFlag)
                        .Select(q => q.PromoTransId).Distinct().ToList();

                    List<ReportPromoTransDetail> listTransId = new();

                    foreach (var loopPromoWOrkflow in listPromoTransDetails)
                    {
                        PromoTrans? promoTrans = new();

                        if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                        {
                            promoTrans = !string.IsNullOrEmpty(companyCode) ?
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.ActiveFlag && q.CompanyCode == companyCode) :
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.ActiveFlag);
                        }
                        else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        {
                            DateTime conStartDate = DateTime.ParseExact(startDate.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            DateTime conEndDate = DateTime.ParseExact(endDate.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                            promoTrans = !string.IsNullOrEmpty(companyCode) ?
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.CompanyCode == companyCode && q.ActiveFlag && q.TransDate >= conStartDate && q.TransDate <= conEndDate) :
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.ActiveFlag && q.TransDate >= conStartDate && q.TransDate <= conEndDate);
                        }
                        else if (!string.IsNullOrEmpty(startDate))
                        {
                            DateTime conStartDate = DateTime.ParseExact(startDate.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                            promoTrans = !string.IsNullOrEmpty(companyCode) ?
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.CompanyCode == companyCode && q.ActiveFlag && q.TransDate >= conStartDate) :
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.ActiveFlag && q.TransDate >= conStartDate);
                        }
                        else if (!string.IsNullOrEmpty(endDate))
                        {
                            DateTime conEndDate = DateTime.ParseExact(endDate.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                            promoTrans = !string.IsNullOrEmpty(companyCode) ?
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.CompanyCode == companyCode && q.ActiveFlag && q.TransDate <= conEndDate) :
                                listPromoTrans.FirstOrDefault(q => q.Id == loopPromoWOrkflow && q.ActiveFlag && q.TransDate <= conEndDate);
                        }

                        if (promoTrans == null)
                            continue;

                        ReportPromoTransDetail dataTransDetail = new()
                        {
                            TransId = promoTrans!.TransId ?? "",
                            TransDate = promoTrans.TransDate ?? DateTime.Now,
                            SiteCode = promoTrans.PromoTransDetail!.FirstOrDefault()?.SiteCode ?? "",
                            ZoneCode = promoTrans.PromoTransDetail!.FirstOrDefault()?.ZoneCode ?? "",
                            PromoUse = promoTrans.PromoTransDetail!.Where(q => q.PromoCode == loopPromoRule.Code)
                             .Sum(q => q.QtyPromo) ?? 0,
                            PromoTotal = promoTrans.PromoTransDetail!.Where(q => q.PromoCode == loopPromoRule.Code)
                             .Sum(q => q.PromoTotal) ?? 0
                        };

                        listTransId.Add(dataTransDetail);
                    }

                    var varQtyUse = listTransId.Sum(q => q.PromoUse);
                    var varBalanceUse = listTransId.Sum(q => q.PromoTotal);

                    ReportPromoTransactionResponse reportPromoResponse = new()
                    {
                        CompanyCode = promoWorkflow.Code,
                        PromoCode = loopPromoRule.Code,
                        PromoName = loopPromoRule.Name,
                        StartDate = loopPromoRule.StartDate,
                        EndDate = loopPromoRule.EndDate,
                        PromoCls = loopPromoRule.Cls,
                        PromoType = loopPromoRule.PromoActionType,
                        QtyPromo = promoMaster?.Qty ?? 0,
                        QtyUse = varQtyUse,
                        QtyAvail = (promoMaster?.Qty ?? 0) != 0 ? promoMaster?.Qty - varQtyUse ?? 0 : 0,
                        BalancePromo = promoMaster?.Balance ?? 0,
                        BalanceUse = varBalanceUse,
                        BalanceAvail = (promoMaster?.Balance ?? 0) != 0 ? promoMaster?.Balance - varBalanceUse ?? 0 : 0,
                        TransactionDetail = listTransId
                    };

                    dataResponse.Add(reportPromoResponse);
                }

                return (dataResponse, "Success");
            }
            catch (Exception ex)
            {
                return (dataResponse, $"Error : {ex.Message}");
            }
        }

        public async Task<(ReportPromoResponse?, string)> GetReportPromoTypeAsync(string? companyCode, string? startDate, string? endDate, string? zoneName = null)
        {
            ReportPromoResponse dataResponse = new();

            try
            {
                (List<ReportPromoTransactionResponse>? dataReportAll, List<ResultDataClassTypeDto>? dataClassType, ICollection<PromoMasterZone>? dataPromoZone, string message) =
                await GetClassTypeCombinationAsync(companyCode, startDate, endDate);

                if (message == null || message != "Success" || dataReportAll == null || dataPromoZone == null ||
                    dataClassType == null || dataReportAll.Count < 1 || dataClassType.Count < 1 || dataPromoZone.Count < 1)
                    return (dataResponse, "Data Master not Incompleted !!");

                if (!string.IsNullOrEmpty(zoneName))
                {
                    PromoMasterZone? promoZone = dataPromoZone.FirstOrDefault(q => q.PricingZoneName!.ToUpper() == zoneName!.ToUpper());

                    if (promoZone == null)
                        return (dataResponse, "Data Zone not Match in Master Zone");

                    foreach (var loopDataReport in dataReportAll)
                    {
                        if (loopDataReport.TransactionDetail != null && loopDataReport.TransactionDetail.Count > 0)
                        {
                            loopDataReport.QtyUse = loopDataReport.TransactionDetail!.Where(q => q.ZoneCode == promoZone!.PricingZoneCode).Sum(q => q.PromoUse);
                            loopDataReport.TransactionDetail = loopDataReport.TransactionDetail!.Where(q => q.ZoneCode == promoZone!.PricingZoneCode).ToList();

                            if (loopDataReport.QtyPromo > 0)
                                loopDataReport.QtyAvail = loopDataReport.QtyPromo - loopDataReport.QtyUse;
                        }
                    }
                }

                int totalPromoUse = dataReportAll!.Sum(q => q.QtyUse);

                List<ReportPromoDetailResponse> listReportPromoDetailResponse = new();

                foreach (var loopDataClassType in dataClassType)
                {
                    ReportPromoDetailResponse reportPromoDetailResponse = new();

                    List<ReportPromoTransactionResponse> dataReportTransaction = dataReportAll!
                        .Where(q => q.PromoCls == Convert.ToInt32(loopDataClassType.LineClass) &&
                                       q.PromoType == loopDataClassType.PromotionTypeKey).ToList();

                    if (dataReportTransaction.Count < 1)
                    {
                        reportPromoDetailResponse = new()
                        {
                            Name = loopDataClassType.PromotionTypeName,
                            PromoUse = 0,
                            PromoUsePercentage = 0,
                        };

                        listReportPromoDetailResponse.Add(reportPromoDetailResponse);

                        continue;
                    }

                    reportPromoDetailResponse = new()
                    {
                        Name = loopDataClassType.PromotionTypeName,
                        PromoUse = dataReportTransaction.Sum(q => q.QtyUse),
                        PromoUsePercentage = (double)dataReportTransaction.Sum(q => q.QtyUse) / (double)totalPromoUse * (double)100,
                    };

                    listReportPromoDetailResponse.Add(reportPromoDetailResponse);
                }

                dataResponse = new()
                {
                    CompanyCode = companyCode ?? "-",
                    StartDate = startDate ?? "-",
                    EndDate = endDate ?? "-",
                    TotalPromoUse = totalPromoUse,
                    Detail = listReportPromoDetailResponse
                };

                return (dataResponse, "Success");
            }
            catch (Exception ex)
            {
                return (dataResponse, $"Error : {ex.Message}");
            }
        }

        public async Task<(ReportPromoResponse?, string)> GetReportPromoTypeDetailAsync(string? companyCode, string? startDate, string? endDate, string? promoTypeName = null)
        {
            ReportPromoResponse reportPromoResponse = new();

            (List<ReportPromoTransactionResponse>? dataReportAll, List<ResultDataClassTypeDto>? dataClassType, ICollection<PromoMasterZone>? dataPromoZone, string message) =
               await GetClassTypeCombinationAsync(companyCode, startDate, endDate);

            if (message == null || message != "Success" || dataReportAll == null ||
                dataClassType == null || dataReportAll.Count < 1 || dataClassType.Count < 1 ||
                dataPromoZone == null || dataPromoZone.Count < 1)
                return (reportPromoResponse, "Data Master not Incompleted !!");

            List<ReportPromoDetailResponse> listReportPromoDetailResponse = new();

            int totalPromoUse = 0;

            if (!string.IsNullOrEmpty(promoTypeName))
            {
                var dataClassTypeSingle = dataClassType!.Find(q => q.PromotionTypeName!.ToUpper() == promoTypeName.ToUpper());

                if (dataClassTypeSingle != null)
                    dataReportAll = dataReportAll!.Where(q => q.PromoCls == dataClassTypeSingle.LineClass &&
                            q.PromoType!.ToUpper() == dataClassTypeSingle.PromotionTypeKey!.ToUpper()).ToList();
            }

            if (dataReportAll.Count > 0)
            {
                totalPromoUse = dataReportAll!.Sum(q => q.QtyUse);

                var zoneDisticnt = dataPromoZone.Select(q => new { q.PricingZoneCode, q.PricingZoneName }).Distinct().ToList();

                foreach (var loopDataPromoZone in zoneDisticnt)
                {
                    int promoUse = 0;

                    foreach (var loopPromoReportAll in dataReportAll)
                    {
                        promoUse += loopPromoReportAll.TransactionDetail!.Where(q => q.ZoneCode == loopDataPromoZone.PricingZoneCode).Sum(q => q.PromoUse);
                    }

                    ReportPromoDetailResponse reportPromoDetailResponse = new()
                    {
                        Name = loopDataPromoZone.PricingZoneName,
                        PromoUse = promoUse,
                        PromoUsePercentage = (double)promoUse / (double)totalPromoUse * (double)100
                    };

                    listReportPromoDetailResponse.Add(reportPromoDetailResponse);
                }
            }

            reportPromoResponse = new()
            {
                CompanyCode = companyCode ?? "-",
                StartDate = startDate ?? "-",
                EndDate = endDate ?? "-",
                TotalPromoUse = totalPromoUse,
                Detail = listReportPromoDetailResponse
            };

            return (reportPromoResponse, "Success");
        }

        public async Task<(ReportPromoUsageResponse?, string)> GetReportPromoUsageAsync(string? companyCode, string? startDate, string? endDate)
        {
            ReportPromoUsageResponse dataResponse = new();

            (List<ReportPromoTransactionResponse>? dataReportAll, List<ResultDataClassTypeDto>? dataClassType, ICollection<PromoMasterZone>? dataPromoZone, string message) =
                await GetClassTypeCombinationAsync(companyCode, startDate, endDate);

            if (message == null || message != "Success" || dataReportAll == null || dataPromoZone == null ||
                dataClassType == null || dataReportAll.Count < 1 || dataClassType.Count < 1 || dataPromoZone.Count < 1)
                return (dataResponse, "Data Master not Incompleted !!");

            List<ReportPromoUsageResponseDetail> listReportDetail = new();

            foreach (var loopPromoReport in dataReportAll)
            {
                ResultDataClassTypeDto dataClass = dataClassType.Find(q => q.PromotionTypeKey == loopPromoReport.PromoType && q.LineClass == loopPromoReport.PromoCls)!;

                ReportPromoUsageResponseDetail reportDetail = new()
                {
                    PromoType = dataClass.PromotionTypeName,
                    PromoCode = loopPromoReport.PromoCode,
                    PromoName = loopPromoReport.PromoName,
                    StartDate = loopPromoReport.StartDate,
                    EndDate = loopPromoReport.EndDate,
                    PromoQuota = loopPromoReport.QtyPromo == 0 ? "-" : loopPromoReport.QtyPromo.ToString(),
                    PromoUse = loopPromoReport.QtyUse.ToString(),
                    PromoAvaible = loopPromoReport.QtyPromo == 0 ? "-" : loopPromoReport.QtyAvail.ToString()
                };

                listReportDetail.Add(reportDetail);
            }

            dataResponse = new()
            {
                CompanyCode = companyCode ?? "-",
                StartDate = startDate ?? "-",
                EndDate = endDate ?? "-",
                ReportDetail = listReportDetail
            };

            return (dataResponse, "Success");
        }

        public async Task<(ReportPromoUsageEntertaimentResponse?, string)> GetReportPromoEntertaimetUsageAsync(string? companyCode, string? nip, string? startDate, string? endDate)
        {
            List<string> listPromoEntertaiment = new();
            ReportPromoUsageEntertaimentResponse dataResponse = new();

            PromoWorkflow? promoWorkflow =
                await _dataDbContext.PromoWorkflow
                .FirstOrDefaultAsync(q => q.Code == companyCode);

            if (promoWorkflow == null)
                return (dataResponse, $"Promo Workflow is Null With Company : {companyCode} !!");

            List<PromoRule>? listPromoRules = await _dataDbContext.PromoRule.Where(q => q.EntertaimentNip == nip && q.Cls == 4).ToListAsync()
                ?? new List<PromoRule>();

            if (listPromoRules.Count < 1)
                return (dataResponse, $"Promo is Null With NIP : {nip} !!");

            foreach (var loopPromoRule in listPromoRules)
            {
                listPromoEntertaiment.Add(loopPromoRule.Code!);
            }

            if (listPromoEntertaiment.Count < 1)
                return (dataResponse, $"Promo Code is Null With NIP : {nip} !!");

            (List<ReportPromoTransactionResponse>? dataReportAll, List<ResultDataClassTypeDto>? dataClassType, ICollection<PromoMasterZone>? dataPromoZone, string message) =
                await GetClassTypeCombinationAsync(companyCode, startDate, endDate, true, listPromoEntertaiment);

            if (message == null || message != "Success" || dataReportAll == null || dataPromoZone == null ||
                dataClassType == null || dataReportAll.Count < 1 || dataClassType.Count < 1 || dataPromoZone.Count < 1)
                return (dataResponse, "Data Master not Incompleted !!");

            List<ReportPromoUsageEntertaimentResponseDetail> listDetail = new();

            foreach (var loopPromoEnteraimentUsage in dataReportAll)
            {
                PromoRule promoRule = listPromoRules.Find(q => q.Code == loopPromoEnteraimentUsage.PromoCode)!;

                ReportPromoUsageEntertaimentResponseDetail dataPromoDetail = new()
                {
                    PromoMonth = promoRule.StartDate.ToString("MMMM", CultureInfo.InvariantCulture),
                    PromoCode = loopPromoEnteraimentUsage.PromoCode,
                    PromoName = loopPromoEnteraimentUsage.PromoName,
                    StartDate = promoRule.StartDate,
                    EndDate = promoRule.EndDate,
                    PromoQuota = loopPromoEnteraimentUsage.BalancePromo,
                    PromoUse = loopPromoEnteraimentUsage.BalanceUse,
                    PromoAvaible = loopPromoEnteraimentUsage.BalanceAvail
                };

                listDetail.Add(dataPromoDetail);
            }

            ReportPromoUsageEntertaimentResponse dataPromo = new()
            {
                CompanyCode = companyCode ?? "-",
                StartDate = startDate ?? "-",
                EndDate = endDate ?? "-",
                ReportDetail = listDetail.OrderBy(q => q.StartDate).ToList()
            };

            return (dataPromo, "Success");
        }

        private async Task<(List<ReportPromoTransactionResponse>?, List<ResultDataClassTypeDto>?, ICollection<PromoMasterZone>?, string)> GetClassTypeCombinationAsync
            (string? companyCode, string? startDate, string? endDate, bool entertaiment = false, List<string>? promoList = null)
        {
            (List<ReportPromoTransactionResponse> dataReportAll, string messageReportAll) =
                await GetReportPromoAsync(companyCode, startDate, endDate, entertaiment, promoList);

            ICollection<PromoMasterClass> dataClass = await _dataDbContext.PromoMasterClass.ToListAsync();
            ICollection<PromoMasterType> dataType = await _dataDbContext.PromoMasterType.ToListAsync();
            ICollection<PromoMasterZone> dataZone = await _dataDbContext.PromoMasterZone.ToListAsync();

            dataReportAll = dataReportAll.Where(q => q.TransactionDetail!.Count > 0).ToList();

            if (messageReportAll != "Success" || dataReportAll.Count < 1 || dataClass!.Count < 1 || dataType!.Count < 1 || dataZone!.Count < 1)
                return (null, null, null, "Data Master or Transaction is Nothing !!");

            var dataClassType =
                (from dtCls in dataClass
                 from dtTyp in dataType.Where(q => q.PromotionClassId!.ToUpper() == dtCls.Id.ToString().ToUpper()).DefaultIfEmpty()
                 select new
                 {
                     LineClass = dtCls.LineNum,
                     dtTyp.PromotionTypeKey,
                     dtTyp.PromotionTypeName,
                 }).ToList();

            List<ResultDataClassTypeDto> listDataClassTypeResponse = new();

            foreach (var loopDataClassType in dataClassType)
            {
                ResultDataClassTypeDto dataClassTypeResponse = new()
                {
                    LineClass = Convert.ToInt32(loopDataClassType.LineClass),
                    PromotionTypeKey = loopDataClassType.PromotionTypeKey,
                    PromotionTypeName = loopDataClassType.PromotionTypeName,
                };

                listDataClassTypeResponse.Add(dataClassTypeResponse);
            }

            return (dataReportAll, listDataClassTypeResponse, dataZone, "Success");
        }
    }
}
