using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<int> AddAsync(Usuario user);
        Task<bool> UpdateAsync(Usuario user);
        Task<bool> RemoveAsync(int id);
    }
}
