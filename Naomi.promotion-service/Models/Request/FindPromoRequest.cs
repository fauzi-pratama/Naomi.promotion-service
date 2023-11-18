
using Naomi.promotion_service.Models.Dto;

namespace Naomi.promotion_service.Models.Request
{
    public class FindPromoRequest
    {
        public string? TransId { get; set; }
        public string? TransDate { get; set; }
        public string? CompanyCode { get; set; }
        public string? SiteCode { get; set; }
        public string? ZoneCode { get; set; }
        public bool EntertaimentStatus { get; set; } = false;
        public string? EntertaimentNip { get; set; }
        public string? EntertaimentOtp { get; set; }
        public string? PromoAppCode { get; set; }
        public Boolean Member { get; set; } = false;
        public Boolean NewMember { get; set; } = false;
        public string? StatusMember { get; set; }
        public List<ItemProduct>? ItemProduct { get; set; }
    }

    public class ItemProduct
    {
        public int LineNo { get; set; }
        public string? SkuCode { get; set; }
        public string? DeptCode { get; set; }
        public double Qty { get; set; }
        public decimal Price { get; set; }
    }
}
