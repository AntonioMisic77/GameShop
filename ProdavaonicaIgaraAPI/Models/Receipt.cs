using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("receipts")]
    public class Receipt
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("cashierid")]
        public int CashierId { get; set; }

        [ForeignKey(nameof(CashierId))]
        public User? Cashier { get; set; }

        [Required]
        [Column("companyid")]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        [StringLength(50)]
        [Column("paymentmethod")]
        public required string PaymentMethod { get; set; }

        [Required]
        [Column("date")]
        public DateTime? Date { get; set; }

        public virtual IEnumerable<ReceiptItem>? ReceiptItems { get; set; }
    }
}
