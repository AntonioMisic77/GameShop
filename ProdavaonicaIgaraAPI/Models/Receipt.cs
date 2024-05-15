using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("receipts")]
    public class Receipt
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int CashierId { get; set; }

        [ForeignKey(nameof(CashierId))]
        public User? Cashier { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }

        [Required]
        [StringLength(50)]
        public int PaymentMethod { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public virtual ICollection<ReceiptItem>? ReceiptItems { get; set; }
    }
}
