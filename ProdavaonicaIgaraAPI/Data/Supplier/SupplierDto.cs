using System.ComponentModel.DataAnnotations;

namespace ProdavaonicaIgaraAPI.Data.Supplier
{
    public class SupplierDto
    {
        #region properties
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

        #endregion
    }
}
