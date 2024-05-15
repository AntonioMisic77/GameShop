using Microsoft.EntityFrameworkCore;

namespace ProdavaonicaIgaraAPI.Models
{
    public class PIGDbContext : DbContext
    {
        public PIGDbContext(DbContextOptions<PIGDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<ReceiptItem> ReceiptItems { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
