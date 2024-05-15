using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("receiptitems")]
    [Index(nameof(ArticleId),IsUnique = true)]
    public class ReceiptItem
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ReceiptId { get; set; }

        [ForeignKey(nameof(ReceiptId))]
        public Receipt? Receipt { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Article? Article { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
