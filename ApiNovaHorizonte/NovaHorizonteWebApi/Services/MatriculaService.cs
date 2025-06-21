using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using NovaHorizonteWebApi.Enums;

namespace ApiNovaHorizonte.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IUserRepository _userRepository;

        public MatriculaService(IMatriculaRepository matriculaRepository, IUserRepository userRepository)
        {
            _matriculaRepository = matriculaRepository;
            _userRepository = userRepository;
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

        public async Task<IEnumerable<Matricula>> GetPendentesAsync()
        {
            return await _matriculaRepository.GetPendentesAsync();
        }

        public async Task<bool> AprovarAsync(int id)
        {
            var matricula = await _matriculaRepository.GetByIdAsync(id);
            if (matricula == null || matricula.Status != "Pendente") return false;

            var novoUsuario = new User
            {
                Nome = matricula.Nome,
                Email = matricula.Email,
                Senha = "123",
                Cargo = UserEnum.Aluno
            };

            await _userRepository.AddAsync(novoUsuario);
            return await _matriculaRepository.UpdateStatusAsync(id, "Aprovado");
        }

        public async Task<bool> RejeitarAsync(int id)
        {
            var matricula = await _matriculaRepository.GetByIdAsync(id);
            if (matricula == null || matricula.Status != "Pendente") return false;

            return await _matriculaRepository.UpdateStatusAsync(id, "Rejeitado");
        }
    }
}
