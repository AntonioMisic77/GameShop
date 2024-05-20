using AutoMapper;
using ProdavaonicaIgaraAPI.Data.User;
using ProdavaonicaIgaraAPI.Models;
using ProdavaonicaIgaraAPI.Repositories;

namespace ProdavaonicaIgaraAPI.Services
{
    public class UserService : IUserService
    {
        #region properties
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public UserService(IUserRepository userRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region methods
        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
        #endregion
    }
}
