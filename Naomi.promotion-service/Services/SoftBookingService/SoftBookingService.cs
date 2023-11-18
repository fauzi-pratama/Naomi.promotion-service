
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Entities;

namespace Naomi.promotion_service.Services.SoftBookingService
{
    public class SoftBookingService : ISoftBookingService
    {
        private readonly DataDbContext _dbContext;

        public SoftBookingService(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<string>, string, bool)> CekPromoAvail(List<PromoRuleCekAvailRequest> listPromo, string companyCode)
        {
            List<string> listPromoAvailResult = new();

            foreach (var loopPromoRequest in listPromo)
            {
                //Jika promo tidak memiliki batas penggunaan
                if (loopPromoRequest.MaxBalance == 0 && loopPromoRequest.MaxUse == 0)
                {
                    listPromoAvailResult.Add(loopPromoRequest.PromoCode!);

                    continue;
                }

                string? promoCekRef = loopPromoRequest.CekRefCode ? loopPromoRequest.RefCode! : loopPromoRequest.PromoCode;

                PromoMaster? promoMaster = await _dbContext.PromoMaster
                    .FirstOrDefaultAsync(q => q.PromoCode == promoCekRef && q.CompanyCode == companyCode && q.ActiveFlag);

                //valide Promo master Booking
                if (promoMaster is null)
                {
                    listPromoAvailResult = new();

                    return (listPromoAvailResult, $"PromoCode : {loopPromoRequest.PromoCode} not Registered on Table PromoMaster !!", false);
                }

                //Cek Balance Promo Avail
                if (loopPromoRequest.MaxBalance != 0 && (promoMaster.BalanceBook + loopPromoRequest.BalanceUse) > promoMaster.Balance)
                    continue;

                //Cek Qty Promo Avail
                if (loopPromoRequest.MaxUse != 0 && (promoMaster.QtyBook + loopPromoRequest.QtyUse) > promoMaster.Qty)
                    continue;

                //Add Promo Avail to List for Return
                listPromoAvailResult.Add(loopPromoRequest.PromoCode!);
            }

            return (listPromoAvailResult, "SUCCESS", true);
        }

        public async Task<(bool, string)> PromoRollBackBeforeCommit(PromoWorkflow promoWorkflow, PromoTrans promoTrans)
        {
            //looping detail transaction untuk rollback data promo master booking
            foreach (var loopPromoTransDetail in promoTrans.PromoTransDetail!)
            {
                //Get Data Promo Master Rule
                PromoRule? promoRule = await _dbContext.PromoRule
                    .FirstOrDefaultAsync(q => q.Code!.ToUpper() == loopPromoTransDetail.PromoCode!.ToUpper() && q.PromoWorkflowId == promoWorkflow.Id && q.ActiveFlag);

                //Jika Data Promo Master Rule atau Data Promo Master Booking Null
                if (promoRule is null)
                    return (false, $"PromoCode : {loopPromoTransDetail.PromoCode} Not Registered on Table promo_rule ");

                //Cek Promo Ref
                if (!string.IsNullOrEmpty(promoRule.RefCode))
                {
                    //Jika terdapat promo ref maka akan diambil data parent promonya
                    promoRule = await _dbContext.PromoRule
                        .FirstOrDefaultAsync(q => q.Code!.ToUpper() == promoRule.RefCode!.ToUpper() && q.PromoWorkflowId == promoWorkflow.Id && q.ActiveFlag);

                    if (promoRule is null)
                        return (false, $"PromoRefCode : {loopPromoTransDetail.PromoCode} Not Registered on Table promo_rule ");
                }

                //Get data promo master booking
                PromoMaster? promoMaster = await _dbContext.PromoMaster
                    .FirstOrDefaultAsync(q => q.PromoCode == promoRule.Code && q.CompanyCode == promoTrans.CompanyCode && q.ActiveFlag);

                //Jika data promo tidak ada batas penggunaan Skip Proses Management Calculate Master Booking
                if ((promoRule.MaxBalance is null || promoRule.MaxBalance == 0) &&
                    (promoRule.MaxUse is null || promoRule.MaxUse == 0))
                    continue;

                //Jika data promo master booking null
                if (promoMaster is null)
                    return (false, $"PromoCode : {loopPromoTransDetail.PromoCode} not Registered on Table promo_master");

                //Execute promo master booking qty
                if (promoRule.MaxUse > 0)
                    promoMaster.QtyBook -= loopPromoTransDetail.QtyPromo;

                //Execute promo master booking balance
                if (promoRule.MaxBalance > 0)
                    promoMaster.BalanceBook -= loopPromoTransDetail.PromoTotal;
            }

            //Delete Data Promo Transaction Detail
            _dbContext.PromoTransDetail.RemoveRange(promoTrans.PromoTransDetail);
            _dbContext.PromoTrans.Remove(promoTrans);

            return (true, "SUCCESS");
        }
    }
}
