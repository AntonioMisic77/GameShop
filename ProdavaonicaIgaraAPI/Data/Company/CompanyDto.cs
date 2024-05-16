using System.ComponentModel.DataAnnotations;

namespace ProdavaonicaIgaraAPI.Data.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(100)]
        public required string Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

    }
}
