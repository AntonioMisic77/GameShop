using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Data.Supplier;
using System.ComponentModel.DataAnnotations;

namespace ProdavaonicaIgaraAPI.Data.Articles
{
    public class ArticleDto
    {
        #region properties
        public int Id { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Precision(10, 2)]
        public double Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public SupplierDto? Supplier { get; set; }

        #endregion
    }
}
