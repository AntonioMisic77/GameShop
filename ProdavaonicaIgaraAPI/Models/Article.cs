using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("articles")]
    public class Article
    {
        #region properties
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("supplierid")]
        public int SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier? Supplier { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public required string Name { get; set; }

        [StringLength(500)]
        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Precision(10,2)]
        [Column("price")]
        public double Price { get; set; }

        [Required]
        [Column("stockquantity")]
        public int StockQuantity { get; set; }
        
        #endregion
    }
}
