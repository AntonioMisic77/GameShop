using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Repositories
{
    public interface IUserRepository
    {
        #region methods

        Task<List<User>> GetUsersAsync();

        #endregion
    }
}
