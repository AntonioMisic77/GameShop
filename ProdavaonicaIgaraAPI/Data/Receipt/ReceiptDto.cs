using ProdavaonicaIgaraAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProdavaonicaIgaraAPI.Data.ReceiptItem;
using ProdavaonicaIgaraAPI.Data.Company;
using ProdavaonicaIgaraAPI.Data.User;

namespace ProdavaonicaIgaraAPI.Data.Receipt
{
    public class ReceiptDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int CashierId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public required string PaymentMethod { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public virtual IEnumerable<ReceiptItemDto>? ReceiptItems { get; set; }


        public CompanyDto? CompanyDto { get; set; }

        public UserDto? Cashier { get; set; }

    }
}
