
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_rule_variable")]
    public class PromoRuleVariable
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
        [Column("code", Order = 3), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("params_expression", Order = 5), DataType("text")]
        public string? ParamsExpression { get; set; }

        [Column("created_date", Order = 6)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 7), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 8)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 9), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 10)]
        public bool ActiveFlag { get; set; }
    }
}
