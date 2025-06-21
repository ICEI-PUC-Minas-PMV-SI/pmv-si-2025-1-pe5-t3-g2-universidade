using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<int> AddAsync(Matricula matricula);
        Task<IEnumerable<Matricula>> GetAllAsync();
        Task<Matricula?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Matricula matricula);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Matricula>> GetPendentesAsync();
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<IEnumerable<string>> GetCursosByAlunoAsync(int userId);
    }
}
