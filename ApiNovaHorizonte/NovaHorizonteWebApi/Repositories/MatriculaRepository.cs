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
                INSERT INTO matricula (nome, email, telefone, curso, status)
                VALUES (@Nome, @Email, @Telefone, @Curso, @Status)
                RETURNING id;";
            return await connection.QuerySingleAsync<int>(sql, matricula);
        }

        public async Task<IEnumerable<Matricula>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM matricula";
            return await connection.QueryAsync<Matricula>(sql);
        }

        public async Task<Matricula?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM matricula WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Matricula>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Matricula matricula)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                UPDATE matricula
                SET nome = @Nome,
                    email = @Email,
                    telefone = @Telefone,
                    curso = @Curso,
                    status = @Status
                WHERE id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, matricula);
            return rowsAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "DELETE FROM matricula WHERE id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Matricula>> GetPendentesAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM matricula WHERE status = 'Pendente'";
            return await connection.QueryAsync<Matricula>(sql);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "UPDATE matricula SET status = @Status WHERE id = @Id";
            var affected = await connection.ExecuteAsync(sql, new { Status = status, Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<string>> GetCursosByAlunoAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = @"
                SELECT curso
                FROM matricula
                WHERE email = (
                    SELECT email FROM usuarios WHERE id = @Id
                )
                AND status = 'Aprovado'
            ";

            var cursos = await connection.QueryAsync<string>(query, new { Id = userId });
            return cursos;
        }
    }
}
