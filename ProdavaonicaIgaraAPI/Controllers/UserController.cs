using Microsoft.AspNetCore.Mvc;
using ProdavaonicaIgaraAPI.Data.User;
using ProdavaonicaIgaraAPI.Services;

namespace ProdavaonicaIgaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region properties
        private readonly IUserService _userService;
        #endregion

        #region ctor
        public UserController(IUserService userService)
        {
            _userService = userService;
        } 
        #endregion

        #region endpoints
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }

        #endregion
    }
}
