
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_rule_result")]
    public class PromoRuleResult
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
        [Column("group_line", Order = 3)]
        public int GroupLine { get; set; }

        [Required]
        [Column("item", Order = 4), MaxLength(50)]
        public string? Item { get; set; }

        [Column("dsc_value", Order = 5), MaxLength(50)]
        public string? DscValue { get; set; }

        [Column("max_value", Order = 6), MaxLength(50)]
        public string? MaxValue { get; set; }

        [Column("link_rsl", Order = 7), MaxLength(50)]
        public string? LinkRsl { get; set; }

        [Column("created_date", Order = 8)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 9), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 10)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 11), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 12)]
        public bool ActiveFlag { get; set; }
    }
}
