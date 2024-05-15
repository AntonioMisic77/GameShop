using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdavaonicaIgaraAPI.Models
{
    [Table("users")]
    [Index(nameof(Username),IsUnique = true)]
    [Index(nameof(Email),IsUnique = true)]
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Username { get; set; }

        [StringLength(50)]
        public string? IBAN { get; set; }

        [Required]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [StringLength(100)]
        public required string PasswordHash { get; set; }

        public virtual ICollection<UserRole>? UserRole { get; set; }
    }
}
