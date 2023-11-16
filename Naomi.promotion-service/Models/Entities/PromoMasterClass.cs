using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_master_class")]
    public class PromoMasterClass
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("promotion_class_key", Order = 1), MaxLength(50)]
        public string? PromotionClassKey { get; set; }

        [Required]
        [Column("promotion_class_name", Order = 2), MaxLength(200)]
        public string? PromotionClassName { get; set; }

        [Column("line_num", Order = 3)]
        public int LineNum { get; set; }

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
