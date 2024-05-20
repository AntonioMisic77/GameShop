using ProdavaonicaIgaraAPI.Data.User;
using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Services
{
    public interface IUserService
    {
        #region methods
        Task<List<UserDto>> GetUsersAsync();
        #endregion
    }
}
