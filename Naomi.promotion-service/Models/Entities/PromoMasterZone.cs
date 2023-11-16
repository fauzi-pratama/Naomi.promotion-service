using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_master_zone")]
    public class PromoMasterZone
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Required]
        [Column("site_code", Order = 2), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Required]
        [Column("pricing_zone_code", Order = 3), MaxLength(50)]
        public string? PricingZoneCode { get; set; }

        [Required]
        [Column("pricing_zone_name", Order = 4), MaxLength(200)]
        public string? PricingZoneName { get; set; }

        [Column("created_date", Order = 5)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 6), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 7)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 8), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 9)]
        public bool ActiveFlag { get; set; }
    }
}
