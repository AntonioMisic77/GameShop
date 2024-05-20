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
        #region properties
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("firstname")]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("lastname")]
        public required string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("username")]
        public string? Username { get; set; }

        [StringLength(50)]
        [Column("iban")]
        public string? IBAN { get; set; }

        [Required]
        [StringLength(100)]
        [Column("email")]
        public required string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Column("passwordhash")]
        public required string PasswordHash { get; set; }

        public virtual ICollection<UserRole>? UserRole { get; set; }

        #endregion
    }
}
