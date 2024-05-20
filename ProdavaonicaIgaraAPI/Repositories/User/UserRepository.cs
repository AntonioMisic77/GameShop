using Microsoft.EntityFrameworkCore;
using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region properties
        private readonly PIGDbContext _context;
        #endregion

        #region ctor
        public UserRepository(PIGDbContext context)
        {
            _context = context;
        }
        #endregion

        #region methods
        public async Task<List<User>> GetUsersAsync()
        {
              return await _context.Users
                                .Include(a => a.UserRole)
                                .ThenInclude(b => b.Role)
                                .Where(a => a.UserRole.Any(b => b.Role.Name == "cashier" || b.Role.Name == "manager"))
                                .ToListAsync();
        }
        #endregion
    }
}
