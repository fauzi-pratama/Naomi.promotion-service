
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Naomi.promotion_service.Models.Dto;
using Naomi.promotion_service.Models.Request;
using Naomi.promotion_service.Models.Response;
using Naomi.promotion_service.Models.Contexts;
using Naomi.promotion_service.Models.Entities;
using Naomi.promotion_service.Services.OtpPromoService;
using Naomi.promotion_service.Services.PromoSetupService;
using Naomi.promotion_service.Services.SoftBookingService;

namespace Naomi.promotion_service.Services.FindPromoService
{
    public class FindPromoService : IFindPromoService
    {
        private readonly IMapper _mapper;
        private readonly IOtpService _otpService;
        private readonly DataDbContext _dataDbContext;
        private readonly IPromoSetupService _promoSetupService;
        private readonly ISoftBookingService _softBookingService;

        public FindPromoService(DataDbContext dataDbContext, IPromoSetupService promoSetupService, IOtpService otpService, ISoftBookingService softBookingService, IMapper mapper)
        {
            _mapper = mapper;
            _otpService = otpService;
            _dataDbContext = dataDbContext;
            _promoSetupService = promoSetupService;
            _softBookingService = softBookingService;
        }

        public async Task<(List<FindPromoResponse>, string, bool)> FindPromo(FindPromoRequest findPromoRequest)
        {
            try
            {
                #region "Cek Company and get data from Promo Workflow"

                //Cek company sudah terdaftar di table promo_workflow atau belum
                PromoWorkflow? promoWorkflow = await _dataDbContext.PromoWorkflow
                    .FirstOrDefaultAsync(q => q.Code!.ToUpper() == findPromoRequest.CompanyCode!.ToUpper() && q.ActiveFlag);

                if (promoWorkflow is null)
                    return (new List<FindPromoResponse>(), $"Company {findPromoRequest.CompanyCode} not Registered on promo_workflow", false);

                #endregion

                #region "Cek and Validation Otp"

                //Cek otp promo sudah sesuai dengan yang di generate lewat email atau belum
                if (findPromoRequest.EntertaimentStatus)
                {
                    (bool cekOtp, string messageOtp) =
                        await _otpService.ConfirmOtp(findPromoRequest.CompanyCode!, findPromoRequest.EntertaimentNip!, findPromoRequest.EntertaimentOtp!);

                    if (!cekOtp)
                        return (new List<FindPromoResponse>(), $"Failed cek Otp : {messageOtp}", false);
                }

                #endregion

                #region "Cek and Rollback if Transaction already before commit"

                //cek tabel transaksi promo jika sudah ada dan belum di commit maka akan di rolback untuk keperluan softbooking
                PromoTrans? promoTransBeforeCommited = await _dataDbContext.PromoTrans
                    .Include(q => q.PromoTransDetail)
                    .FirstOrDefaultAsync(q => q.TransId!.ToUpper() == findPromoRequest.TransId!.ToUpper() && !q.Commited && q.ActiveFlag);

                if (promoTransBeforeCommited is not null)
                {
                    (bool cekRollBack, string messageRollBack) = await _softBookingService.PromoRollBackBeforeCommit(promoWorkflow, promoTransBeforeCommited);

                    if (!cekRollBack)
                        return (new List<FindPromoResponse>(), $"Failed rollback transaction id {findPromoRequest.TransId} : {messageRollBack}", false);
                }

                #endregion

                #region "Mapper Data Request to Params Engine and Bypass Mop"

                //Mapper Request ke Dto
                ParamsPromoDto paramsPromoDto = _mapper.Map<ParamsPromoDto>(findPromoRequest);

                //Mengambil data list mop untuk di bypass agar promo mop keluar tanpa harus memasukan data mop
                List<Mop> listMop = new();
                List<PromoMasterMop> listPromoMop = await _dataDbContext.PromoMasterMop
                    .Where(q => q.CompanyCode!.ToUpper() == findPromoRequest.CompanyCode!.ToUpper() && q.SiteCode!.ToUpper() == findPromoRequest.SiteCode!.ToUpper()
                        && q.ActiveFlag)
                    .ToListAsync();

                foreach (var loopPromoMop in listPromoMop)
                {
                    Mop mop = new()
                    {
                        MopCode = loopPromoMop.MopCode,
                        Amount = findPromoRequest.ItemProduct!.Sum(q => Convert.ToDecimal(q.Qty) * q.Price)
                    };

                    listMop.Add(mop);
                }

                paramsPromoDto.Mop = listMop;

                #endregion

                #region "Run Engine to Find Promo and Mapper to Result Promo"

                //Call Engine untuk mendapatkan promo
                List<RulesEngine.Models.RuleResultTree>? listPromoResult = await _promoSetupService.GetPromo(paramsPromoDto.CompanyCode!, paramsPromoDto);

                if (listPromoResult is null || listPromoResult.Count < 1)
                    return (new List<FindPromoResponse>(), "No Have Promo for This Cart", true);

                List<ResultPromoDto> listResultPromoDtos = _mapper.Map<List<ResultPromoDto>>(listPromoResult);

                #endregion

                #region "Cek Quota Promo"

                //Cek promo masih tersedia atau tidak jika promo tersebut memiliki batas penggunaan

                List<PromoRuleCekAvailRequest> listPromoCekAvail = new();

                //Cek promo memiliki ref ke parent promo atau tidak
                foreach (var loopListPromoResult in listResultPromoDtos)
                {
                    PromoRule? cekPromoRef = await _dataDbContext.PromoRule.FirstOrDefaultAsync(q => q.Code == loopListPromoResult.PromoCode!
                        && q.PromoWorkflowId == promoWorkflow!.Id && q.ActiveFlag);

                    PromoRuleCekAvailRequest promoCekAvail = new()
                    {
                        PromoCode = loopListPromoResult.PromoCode,
                        BalanceUse = loopListPromoResult.PromoListItem!.Count < 1 ? 0 : (decimal)loopListPromoResult.PromoListItem!.FirstOrDefault()!.TotalDiscount!,
                        QtyUse = cekPromoRef!.MultipleQty == 0 ? 1 : cekPromoRef.MultipleQty,
                        MaxBalance = (decimal)loopListPromoResult.MaxBalance!,
                        MaxUse = (int)loopListPromoResult.MaxUse!,
                        CekRefCode = cekPromoRef!.RefCode != null,
                        RefCode = cekPromoRef!.RefCode != null ? cekPromoRef.RefCode : null
                    };

                    listPromoCekAvail.Add(promoCekAvail);
                }

                (List<string> listPromoAvail, string messageCekQuota, bool cekQuota) = await _softBookingService.CekPromoAvail(listPromoCekAvail, findPromoRequest.CompanyCode!);

                if (!cekQuota)
                    return (new List<FindPromoResponse>(), $"Failed cek Quota Promo : {messageCekQuota}", false);

                //Filter Promo Result dengan promo avail
                listResultPromoDtos = listResultPromoDtos.Where(q => listPromoAvail.Contains(q.PromoCode!)).ToList();

                if (listResultPromoDtos is null || listResultPromoDtos.Count < 1)
                    return (new List<FindPromoResponse>(), "No Have Promo for This Cart", true);

                #endregion

                List<FindPromoResponse> listFindPromoResponses = _mapper.Map<List<FindPromoResponse>>(listResultPromoDtos);

                await _dataDbContext.SaveChangesAsync();

                return (listFindPromoResponses, "SUCCESS", true);
            }
            catch (Exception ex)
            {
                return (new List<FindPromoResponse>(), ex.Message, false);
            }
        }
    }
}
