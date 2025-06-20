using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> AddAsync(Usuario user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _userRepository.RemoveAsync(id);
        }

        public async Task<bool> UpdateAsync(Usuario user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}
