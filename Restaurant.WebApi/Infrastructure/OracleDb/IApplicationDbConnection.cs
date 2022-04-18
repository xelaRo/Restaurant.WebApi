using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public interface IApplicationDbConnection
    {
        Task<IDbConnection> GetConnection();
        //Task<IDbConnection> GetDwConnection();
        void CloseConnection(IDbConnection conn);
    }
}