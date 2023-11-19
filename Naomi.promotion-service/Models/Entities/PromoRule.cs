
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_rule")]
    public class PromoRule
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("PromoWorkflow")]
        [Column("promo_workflow_id", Order = 1)]
        public Guid? PromoWorkflowId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Column("redeem_code", Order = 3), MaxLength(50)]
        public string? RedeemCode { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("cls", Order = 5)]
        public int Cls { get; set; }

        [Required]
        [Column("lvl", Order = 6)]
        public int Lvl { get; set; }

        [Required]
        [Column("item_type", Order = 7), MaxLength(50)]
        public string? ItemType { get; set; }

        [Required]
        [Column("result_type", Order = 8), MaxLength(50)]
        public string? ResultType { get; set; }

        [Required]
        [Column("promo_action_type", Order = 9), MaxLength(50)]
        public string? PromoActionType { get; set; }

        [Column("promo_action_value", Order = 10), DataType("text")]
        public string? PromoActionValue { get; set; }

        [Column("max_value", Order = 11), MaxLength(50)]
        public string? MaxValue { get; set; }

        [Column("max_multiple", Order = 12)]
        public int? MaxMultiple { get; set; }

        [Column("max_use", Order = 13)]
        public int? MaxUse { get; set; }

        [Column("max_balance", Order = 14)]
        public decimal? MaxBalance { get; set; }

        [Column("max_discount", Order = 15)]
        public DateTime StartDate { get; set; }

        [Column("end_date", Order = 16)]
        public DateTime EndDate { get; set; }

        [Column("ref_code", Order = 17), MaxLength(50)]
        public string? RefCode { get; set; }

        [Column("multiple_qty", Order = 18)]
        public int MultipleQty { get; set; }

        [Column("entertaiment_nip", Order = 19), MaxLength(50)]
        public string? EntertaimentNip { get; set; }

        [Column("promo_desc", Order = 20), DataType("text")]
        public string? PromoDesc { get; set; }

        [Column("promo_term_condition", Order = 21), DataType("text")]
        public string? PromoTermCondition { get; set; }

        [Column("promo_show", Order = 22)]
        public bool PromoShow { get; set; } = false;

        [Column("member", Order = 23)]
        public bool Member { get; set; }

        [Column("new_member", Order = 24)]
        public bool NewMember { get; set; }

        [Column("promo_image_link", Order = 25), DataType("text")]
        public string? PromoImageLink { get; set; }

        [Column("created_date", Order = 26)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 27), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 28)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 29), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 30)]
        public bool ActiveFlag { get; set; }

        public List<PromoRuleMop>? PromoRuleMop { get; set; }
        public List<PromoRuleVariable>? PromoRuleVariable { get; set; }
        public List<PromoRuleExpression>? PromoRuleExpression { get; set; }
        public List<PromoRuleResult>? PromoRuleResult { get; set; }
        public List<PromoRuleApps>? PromoRuleApps { get; set; }
        public List<PromoRuleMembership>? PromoRuleMembership { get; set; }
        public List<PromoRuleSite>? PromoRuleSite { get; set; }
    }
}
