
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Models.Request;

namespace Naomi.promotion_service.Services.CommitService
{
    public class CommitService : ICommitService
    {
        private readonly DataDbContext _dataDbContext;

        public CommitService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<(bool, string)> CommitPromoAsync(CommitRequest commitRequest)
        {
            try
            {
                PromoTrans? promoTrans = _dataDbContext.PromoTrans
                .Include(q => q.PromoTransDetail)
                .FirstOrDefault(q => q.TransId == commitRequest.TransId && q.CompanyCode == commitRequest.CompanyCode && q.ActiveFlag);

                if (promoTrans is null)
                    return (false, $"Data Promo Trans With Trans Id {commitRequest.TransId} is Nothing !!");

                if (promoTrans.Commited)
                    return (true, $"Trans Id {commitRequest.TransId} Already Commited !!");

                if (promoTrans.PromoTransDetail is not null && promoTrans.PromoTransDetail.Count > 0)
                {
                    bool cekPromoEntertaiment = false;
                    string promoOtpCode = "";

                    foreach (var loopEnteraiment in promoTrans.PromoTransDetail)
                    {
                        PromoRule? promoRule = await _dataDbContext.PromoRule.FirstOrDefaultAsync(q => q.Code == loopEnteraiment.PromoCode && q.ActiveFlag);

                        if (promoRule is null)
                            return (false, $"Promo Rule With Id {loopEnteraiment.PromoCode} is Nothing !!");

                        if (promoRule.Cls == 4)
                        {
                            if (string.IsNullOrEmpty(loopEnteraiment.PromoOtp))
                                return (false, $"Promo Trans With Code {loopEnteraiment.PromoOtp} is Nothing !!");

                            cekPromoEntertaiment = true;
                            promoOtpCode = loopEnteraiment.PromoOtp;

                            break;
                        }
                    }

                    if (cekPromoEntertaiment)
                    {
                        PromoOtp? promoOtp = await _dataDbContext.PromoOtp.OrderByDescending(q => q.ExpDate)
                            .FirstOrDefaultAsync(q => q.CompanyCode == commitRequest.CompanyCode && q.Code == promoOtpCode && !q.IsUse && q.ActiveFlag);

                        if (promoOtp == null)
                            return (true, $"Promo Otp With Code {promoOtpCode} is Nothing !!");

                        promoOtp.IsUse = true;
                    }
                }

                promoTrans.Commited = true;

                await _dataDbContext.SaveChangesAsync();

                return (true, "SUCCESS");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string)> CancelPromoAsync(CancelPromoRequest cancelPromoRequest, bool commited = false)
        {
            //Get Data Transaction
            PromoTrans? promoTrans = await _dataDbContext.PromoTrans.Include(q => q.PromoTransDetail)
                .FirstOrDefaultAsync(q => q.TransId == cancelPromoRequest.TransId && q.CompanyCode == cancelPromoRequest.CompanyCode && q.ActiveFlag);

            //Validate Data Transaction
            if (promoTrans is null)
                return (false, $"Data Trans With Trans Id {cancelPromoRequest.TransId} is Nothing !!");

            if (commited && !promoTrans.Commited)
                return (false, $"Trans Id {cancelPromoRequest.TransId} Not Commited !!");

            if (!commited && promoTrans.Commited)
                return (false, $"Trans Id {cancelPromoRequest.TransId} is Already Commited !!");

            if (promoTrans.PromoTransDetail is null)
                return (false, $"Data Trans Detail With Trans Id {cancelPromoRequest.TransId} is Nothing !!");

            //Varible Tampungan Promo Yang Digunakan di tabel Transacrion
            List<string> listPromoUse = new();

            //Isi Varibale Tampungan
            foreach (var loopPromoTransDetail in promoTrans.PromoTransDetail!)
            {
                listPromoUse.Add(loopPromoTransDetail.PromoCode!);
            }

            PromoWorkflow? promoWorkflow = await _dataDbContext.PromoWorkflow
                .FirstOrDefaultAsync(q => q.Code == cancelPromoRequest.CompanyCode);

            if (promoWorkflow == null)
                return (false, $"Promo Workflow {cancelPromoRequest.CompanyCode}");

            //Get Data Promo Master Rule Sesuai Promo Tampungan
            List<PromoRule> listPromoRule = await _dataDbContext.PromoRule
                .Where(q => q.PromoWorkflowId == promoWorkflow.Id && listPromoUse.Contains(q.Code!) && q.ActiveFlag)
                .ToListAsync();

            if (listPromoUse.Count > 0 && (listPromoRule is null || listPromoRule.Count < 1))
                return (false, $"Data Master Promo at Trans Id {cancelPromoRequest.TransId} is Nothing !!");

            //Rollback Data di Promo Master Booking
            foreach (var loopPromoTransDetail in promoTrans.PromoTransDetail!) //looping detail transaction untuk baca promo
            {
                //Get Data Promo Master Rule & Promo Master Booking
                PromoRule? promoRule = listPromoRule.Find(q => q.Code == loopPromoTransDetail.PromoCode);
                PromoMaster? promoMaster = await _dataDbContext.PromoMaster
                    .FirstOrDefaultAsync(q => q.CompanyCode == cancelPromoRequest.CompanyCode && q.PromoCode == loopPromoTransDetail.PromoCode && q.ActiveFlag);

                //Jika Data Promo Master Rule atau Data Promo Master Booking Null
                if (promoRule == null)
                    return (false, $"Promo PromoId : {loopPromoTransDetail.PromoCode} No Have Data Promo Master Rule");

                //Jika Data promo tidak ada batas penggunaan Skip Proses Management Calculate Master Booking
                if ((promoRule.MaxBalance is null || promoRule.MaxBalance == 0) &&
                    (promoRule.MaxUse is null || promoRule.MaxUse == 0))
                    continue;

                //Jika Data Promo Master Booking Null
                if (promoMaster is null)
                    return (false, $"Promo PromoId : {loopPromoTransDetail.PromoCode} No Have Data Promo Master Booking");

                //Execute Promo Master Booking Qty
                if (promoRule.MaxUse > 0)
                    promoMaster.QtyBook -= loopPromoTransDetail.QtyPromo;

                //Execute promo Master Booking Balance
                if (promoRule.MaxBalance > 0)
                    promoMaster.BalanceBook -= loopPromoTransDetail.PromoTotal;
            }

            //Rollback Data di Promo Transaction
            if (!commited)
            {
                //Rollback yang belum commited
                _dataDbContext.PromoTrans.RemoveRange(promoTrans);
                _dataDbContext.PromoTransDetail.RemoveRange(promoTrans.PromoTransDetail);
            }
            else
            {
                //Rollback yang sudah commited
                promoTrans.ActiveFlag = false;

                foreach (var loopPromoTransDetail in promoTrans.PromoTransDetail!)
                {
                    loopPromoTransDetail.ActiveFlag = false;
                }
            }

            await _dataDbContext.SaveChangesAsync();

            return (true, "SUCCESS");
        }
    }
}
