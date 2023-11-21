
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Naomi.promotion_service.Models.Entities
{
    [Table("promo_master")]
    public class PromoMaster
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Required]
        [Column("promo_code", Order = 2), MaxLength(50)]
        public string? PromoCode { get; set; }

        [Column("qty", Order = 3)]
        public int? Qty { get; set; }

        [Column("qty_book", Order = 4)]
        public int? QtyBook { get; set; }

        [Column("balance", Order = 5)]
        public decimal? Balance { get; set; }

        [Column("balance_book", Order = 6)]
        public decimal? BalanceBook { get; set; }

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
