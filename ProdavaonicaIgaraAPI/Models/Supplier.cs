using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("suppliers")]
    [Index(nameof(Email), IsUnique = true)]
    public class Supplier
    {
        #region properties
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public required string Name { get; set; }

        [StringLength(500)]
        [Column("address")]
        public string? Address { get; set; }

        [StringLength(50)]
        [Column("iban")]
        public string? IBAN { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public required string Email { get; set; }

        #endregion
    }
}
