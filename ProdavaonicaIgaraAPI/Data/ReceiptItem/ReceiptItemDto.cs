using ProdavaonicaIgaraAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProdavaonicaIgaraAPI.Data.Articles;

namespace ProdavaonicaIgaraAPI.Data.ReceiptItem
{
    public class ReceiptItemDto
    {
        #region properties
        public int Id { get; set; }

        [Required]
        public int ReceiptId { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public ArticleDto? Article { get; set; }

        #endregion
    }
}
