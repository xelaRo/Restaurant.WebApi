using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public class RestaurantDbConnection : IRestaurantDbConnection
    {
        private readonly IConfiguration _configuration;
        public RestaurantDbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDbConnection> GetOltpConnection()
        {
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("RestaurantDB").Value;

            var conn = new OracleConnection(connectionString);
            
            if(conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            return conn;
        }

        public async Task<IDbConnection> GetDwConnection()
        {
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("RestaurantDW").Value;

            var conn = new OracleConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            return conn;
        }


        public void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }
    }
}
