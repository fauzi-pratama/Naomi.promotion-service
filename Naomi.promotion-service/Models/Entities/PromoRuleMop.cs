
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_rule_mop")]
    public class PromoRuleMop
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_rule")]
        [Column("promo_rule_id", Order = 1)]
        public Guid? PromoRuleId { get; set; }

        [Required]
        [Column("line_num", Order = 2)]
        public int LineNum { get; set; }

        [Required]
        [Column("mop_promo_selection_code", Order = 3), MaxLength(50)]
        public string? MopPromoSelectionCode { get; set; }

        [Required]
        [Column("mop_promo_selection_name", Order = 4), MaxLength(200)]
        public string? MopPromoSelectionName { get; set; }

        [Required]
        [Column("mop_group_code", Order = 5), MaxLength(50)]
        public string? MopGroupCode { get; set; }

        [Required]
        [Column("mop_group_name", Order = 6), MaxLength(200)]
        public string? MopGroupName { get; set; }

        [Column("created_date", Order = 7)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 8), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 9)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 10), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 11)]
        public bool ActiveFlag { get; set; }
    }
}
