using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Dapper;

namespace ApiNovaHorizonte.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MatriculaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(Matricula matricula)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                INSERT INTO Matricula (Nome, Email, Telefone, Curso, Status)
                VALUES (@Nome, @Email, @Telefone, @Curso, @Status);
                SELECT CAST(SCOPE_IDENTITY() AS int);";
            return await connection.QuerySingleAsync<int>(sql, matricula);
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Matricula";
            return await connection.QueryAsync<Matricula>(sql);
        }

        public async Task<Matricula?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Matricula WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Matricula>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Matricula matricula)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                UPDATE Matricula
                SET Nome = @Nome,
                    Email = @Email,
                    Telefone = @Telefone,
                    Curso = @Curso,
                    Status = @Status
                WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, matricula);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "DELETE FROM Matricula WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Matricula>> GetPendentesAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Matricula WHERE Status = 'Pendente'";
            return await connection.QueryAsync<Matricula>(sql);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "UPDATE Matricula SET Status = @Status WHERE Id = @Id";
            var affected = await connection.ExecuteAsync(sql, new { Status = status, Id = id });
            return affected > 0;
        }
    }
}
