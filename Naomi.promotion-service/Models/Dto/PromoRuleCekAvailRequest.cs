
namespace Naomi.promotion_service.Models.Dto
{
    public class PromoRuleCekAvailRequest
    {
        public string? PromoCode { get; set; }
        public decimal BalanceUse { get; set; }
        public int QtyUse { get; set; }
        public decimal MaxBalance { get; set; }
        public int MaxUse { get; set; }
        public string? RefCode { get; set; }
        public bool CekRefCode { get; set; }
    }
}
