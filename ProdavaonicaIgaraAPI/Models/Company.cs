using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("company")]
    public class Company
    {
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

        [StringLength(100)]
        [Column("email")]
        public required string Email { get; set; }

        [StringLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

    }
}
