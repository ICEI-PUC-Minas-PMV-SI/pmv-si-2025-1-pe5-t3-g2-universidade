using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> RemoveAsync(int id);
        Task<User?> LoginAsync(string email, string senha);
        Task<string?> LoginJwtAsync(string email, string senha);
    }
}
