
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_rule_apps")]
    public class PromoRuleApps
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_rule")]
        [Column("promo_rule_id", Order = 1), MaxLength(50)]
        public Guid? PromoRuleId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }

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
