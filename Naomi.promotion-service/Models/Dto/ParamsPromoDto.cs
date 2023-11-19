﻿
namespace Naomi.promotion_service.Models.Dto
{
    public class ParamsPromoDto
    {
        public string? TransId { get; set; }
        public string? TransDate { get; set; }
        public string? CompanyCode { get; set; }
        public string? SiteCode { get; set; }
        public string? ZoneCode { get; set; }
        public bool EntertaimentStatus { get; set; }
        public string? EntertaimentNip { get; set; }
        public string? EntertaimentOtp { get; set; }
        public string? PromoAppCode { get; set; }
        public Boolean Member { get; set; } = false;
        public Boolean NewMember { get; set; } = false;
        public string? StatusMember { get; set; }
        public List<ItemProductDto>? ItemProduct { get; set; }
        public List<Mop>? Mop { get; set; }
    }

    public class ItemProductDto
    {
        public int LineNo { get; set; }
        public string? SkuCode { get; set; }
        public string? DeptCode { get; set; }
        public double Qty { get; set; }
        public decimal Price { get; set; }
    }

    public class Mop
    {
        public string? MopCode { get; set; }
        public decimal Amount { get; set; }
    }
}
