
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_trans")]
    public class PromoTrans
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Required]
        [Column("trans_id", Order = 2), MaxLength(50)]
        public string? TransId { get; set; }

        [Column("trans_date", Order = 3)]
        public DateTime? TransDate { get; set; }

        [Column("commited", Order = 4)]
        public bool Commited { get; set; } = false;

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

        public ICollection<PromoTransDetail>? PromoTransDetail { get; set; }
    }
}
