using Microsoft.AspNetCore.Mvc;
using ProdavaonicaIgaraAPI.Data.User;
using ProdavaonicaIgaraAPI.Services;

namespace ProdavaonicaIgaraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }   

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }
    }
}
