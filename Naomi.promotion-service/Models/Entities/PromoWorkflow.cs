
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_workflow")]
    public class PromoWorkflow
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Column("code", Order = 1), MaxLength(50)]
        public string? Code { get; set; }

        [Column("name", Order = 2), MaxLength(200)]
        public string? Name { get; set; }

        [Column("created_date", Order = 3)]
        public DateTime CreatedDate { get; set; }

        [Column("created_by", Order = 4), MaxLength(50)]
        public string? CreatedBy { get; set; }

        [Column("updated_date", Order = 5)]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by", Order = 6), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag", Order = 7)]
        public bool ActiveFlag { get; set; }

        public List<PromoWorkflowExpression>? PromoWorkflowExpression { get; set; }

        public List<PromoRule>? PromoRule { get; set; }
    }
}
