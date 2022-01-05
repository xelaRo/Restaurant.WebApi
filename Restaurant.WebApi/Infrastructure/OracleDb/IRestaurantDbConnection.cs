using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public interface IRestaurantDbConnection
    {
        Task<IDbConnection> GetOltpConnection();
        Task<IDbConnection> GetDwConnection();
        void CloseConnection(IDbConnection conn);
    }
}