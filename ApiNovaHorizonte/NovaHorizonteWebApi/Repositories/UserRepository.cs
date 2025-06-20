using ApiNovaHorizonte.Interfaces;
using ApiNovaHorizonte.Models;
using Dapper;

namespace ApiNovaHorizonte.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddAsync(Usuario user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO Usuarios  (Nome,Email) VALUES (@Nome, @Email);
                       SELECT CAST(SCOPE_IDENTITY() AS int);";
            return await connection.QuerySingleAsync<int>(sql, user);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuarios";
            return await connection.QueryAsync<Usuario>(sql);
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await connection.QuerySingleAsync<Usuario>(sql, new {Id = id});
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "DELETE FROM Usuarios WHERE Id = @Id";
            var rowsAffected =  await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(Usuario user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "UPDATE Usuarios SET Nome = @Nome, Email = @Email Where Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, user);
            return rowsAffected > 0;
        }
    }
}
