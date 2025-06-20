using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> RemoveAsync(int id);
    }
}
