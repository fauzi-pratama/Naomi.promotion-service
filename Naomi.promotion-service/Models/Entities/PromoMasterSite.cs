using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_master_site")]
    public class PromoMasterSite
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Column("site_code", Order = 2), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Column("site_name", Order = 3), MaxLength(200)]
        public string? SiteName { get; set; }

        [Column("created_date", Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 5), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 6)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 7), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 8)]
        public bool ActiveFlag { get; set; }
    }
}
