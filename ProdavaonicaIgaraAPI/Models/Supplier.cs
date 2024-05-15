using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("suppliers")]
    [Index(nameof(Email), IsUnique = true)]
    public class Supplier
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? IBAN { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }
        
    }
}
