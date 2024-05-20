using ProdavaonicaIgaraAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ProdavaonicaIgaraAPI.Data.User
{
    public class UserDto
    {
        #region properties
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

        #endregion
    }
}
