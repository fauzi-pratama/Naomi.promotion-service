using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_otp")]
    public class PromoOtp
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Column("nip", Order = 2), MaxLength(50)]
        public string? Nip { get; set; }

        [Column("code", Order = 3), MaxLength(50)]
        public string? Code { get; set; }

        [Column("is_use", Order = 4)]
        public bool IsUse { get; set; }

        [Column("exp_date", Order = 5)]
        public DateTime ExpDate { get; set; }

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
