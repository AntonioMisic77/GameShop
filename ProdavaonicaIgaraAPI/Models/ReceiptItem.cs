using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("receiptitems")]
    [Index(nameof(ArticleId),nameof(ReceiptId),IsUnique = true)]
    public class ReceiptItem
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("receiptid")]
        public int ReceiptId { get; set; }

        [ForeignKey(nameof(ReceiptId))]
        public Receipt? Receipt { get; set; }

        [Required]
        [Column("articleid")]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Article? Article { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
