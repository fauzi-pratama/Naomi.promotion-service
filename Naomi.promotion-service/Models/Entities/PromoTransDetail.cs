
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_trans_detail")]
    public class PromoTransDetail
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_trans")]
        [Column("promo_trans_id", Order = 1)]
        public Guid? PromoTransId { get; set; }

        [Required]
        [Column("line_num", Order = 2)]
        public int LineNum { get; set; }

        [Required]
        [Column("promo_code", Order = 3), MaxLength(50)]
        public string? PromoCode { get; set; }

        [Column("promo_otp", Order = 4), MaxLength(50)]
        public string? PromoOtp { get; set; }

        [Required]
        [Column("zone_code", Order = 5), MaxLength(50)]
        public string? ZoneCode { get; set; }

        [Required]
        [Column("site_code", Order = 6), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Column("member", Order = 7)]
        public bool Member { get; set; }

        [Column("new_member", Order = 8)]
        public bool NewMember { get; set; }

        [Column("member_status", Order = 9), MaxLength(50)]
        public string? MemberStatus { get; set; }

        [Column("promo_apps", Order = 10), MaxLength(50)]
        public string? PromoApps { get; set; }

        [Column("qty_promo", Order = 11)]
        public int? QtyPromo { get; set; }

        [Column("promo_total", Order = 12)]
        public decimal? PromoTotal { get; set; }

        [Column("created_date", Order = 13)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 14), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 15)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 16), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 17)]
        public bool ActiveFlag { get; set; }
    }
}
