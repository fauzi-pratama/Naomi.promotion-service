
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_workflow_expression")]
    public class PromoWorkflowExpression
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_workflow")]
        [Column("promo_workflow_id", Order = 1)]
        public Guid? PromoWorkflowId { get; set; }

        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }

        [Column("expression", Order = 4), DataType("text")]
        public string? Expression { get; set; }

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
