namespace Naomi.promotion_service.Models.Response
{
    public class ReportPromoTransactionResponse
    {
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PromoCls { get; set; }
        public string? PromoType { get; set; }
        public int QtyPromo { get; set; }
        public int QtyUse { get; set; }
        public int QtyAvail { get; set; }
        public decimal BalancePromo { get; set; }
        public decimal BalanceUse { get; set; }
        public decimal BalanceAvail { get; set; }
        public List<ReportPromoTransDetail>? TransactionDetail { get; set; }
    }

    public class ReportPromoTransDetail
    {
        public string? TransId { get; set; }
        public DateTime TransDate { get; set; }
        public string? ZoneCode { get; set; }
        public string? SiteCode { get; set; }
        public int PromoUse { get; set; }
        public decimal PromoTotal { get; set; }
    }
}
