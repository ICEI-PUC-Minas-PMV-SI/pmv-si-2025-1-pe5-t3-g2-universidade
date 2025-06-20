using System.Data.Common;

namespace ApiNovaHorizonte.Interfaces
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
