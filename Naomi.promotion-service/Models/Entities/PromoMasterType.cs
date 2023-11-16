
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_master_type")]
    public class PromoMasterType
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_master_class")]
        [Column("promotion_class_id", Order = 1), MaxLength(50)]
        public string? PromotionClassId { get; set; }

        [Required]
        [Column("promotion_type_key", Order = 2), MaxLength(50)]
        public string? PromotionTypeKey { get; set; }

        [Required]
        [Column("promotion_type_name", Order = 3), MaxLength(200)]
        public string? PromotionTypeName { get; set; }

        [Column("line_num", Order = 4)]
        public int LineNum { get; set; }

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
