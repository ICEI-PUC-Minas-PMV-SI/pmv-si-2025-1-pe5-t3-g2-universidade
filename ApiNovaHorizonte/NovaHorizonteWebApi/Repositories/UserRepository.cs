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

        public async Task<int> AddAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                INSERT INTO Usuarios (Nome, Email, Senha, Cargo) 
                VALUES (@Nome, @Email, @Senha, @Cargo)
                RETURNING Id;";
            return await connection.QuerySingleAsync<int>(sql, user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuarios";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await connection.QuerySingleAsync<User>(sql, new { Id = id });
        }

        public async Task<bool> RemoveAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "DELETE FROM Usuarios WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"
                UPDATE Usuarios
                SET Nome = @Nome,
                    Email = @Email,
                    Senha = @Senha,
                    Cargo = @Cargo
                WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(sql, user);
            return rowsAffected > 0;
        }

        public async Task<User?> GetByEmailAndSenhaAsync(string email, string senha)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = "SELECT * FROM Usuarios WHERE Email = @Email AND Senha = @Senha";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email, Senha = senha });
        }
    }
}
