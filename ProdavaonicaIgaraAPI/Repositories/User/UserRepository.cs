using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PIGDbContext _context;

        public UserRepository(PIGDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
              return await _context.Users
                                .Include(a => a.UserRole)
                                .ThenInclude(b => b.Role)
                                .Where(a => a.UserRole.Any(b => b.Role.Name == "cashier" || b.Role.Name == "manager"))
                                .ToListAsync();
        }
    }
}
