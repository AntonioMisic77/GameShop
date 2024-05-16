using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }
}
