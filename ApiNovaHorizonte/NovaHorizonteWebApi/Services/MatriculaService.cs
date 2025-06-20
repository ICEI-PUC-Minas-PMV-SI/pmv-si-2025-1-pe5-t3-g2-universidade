using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;

namespace ApiNovaHorizonte.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public async Task<int> AddAsync(Matricula matricula)
        {
            return await _matriculaRepository.AddAsync(matricula);
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            return await _matriculaRepository.GetAllAsync();
        }

        public async Task<Matricula?> GetByIdAsync(int id)
        {
            return await _matriculaRepository.GetByIdAsync(id);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _matriculaRepository.RemoveAsync(id);
        }

        public async Task<bool> UpdateAsync(Matricula matricula)
        {
            return await _matriculaRepository.UpdateAsync(matricula);
        }
    }
}
