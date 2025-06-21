using ApiNovaHorizonte.Interfaces;
using Npgsql;
using System.Data.Common;

namespace ApiNovaHorizonte.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
