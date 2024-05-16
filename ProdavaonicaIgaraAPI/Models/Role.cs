using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("roles")]
    [Index(nameof(Name),IsUnique = true)]
    public class Role
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("name")]
        public required string Name { get; set; }
    }
}
