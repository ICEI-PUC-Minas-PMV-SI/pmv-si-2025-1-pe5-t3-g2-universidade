using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IMatriculaService
    {
        Task<int> AddAsync(Matricula matricula);
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Matricula matricula);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Matricula>> GetPendentesAsync();
        Task<bool> AprovarAsync(int id);
        Task<bool> RejeitarAsync(int id);
    }
}
