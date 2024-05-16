using ProdavaonicaIgaraAPI.Data.User;
using ProdavaonicaIgaraAPI.Models;

namespace ProdavaonicaIgaraAPI.Services
{
    public interface IUserService
    {

        Task<List<UserDto>> GetUsersAsync();
    }
}
