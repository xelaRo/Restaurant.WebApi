using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public class DbConnection 
    {
        public string ConnectionString { get; }
        public DbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        public async Task<IDbConnection> GetConnection()
        {
            var conn = new OracleConnection(ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }

            return conn;
        }

        //public async Task<IDbConnection> GetDwConnection()
        //{
        //    var conn = new OracleConnection(ConnectionString);

        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        await conn.OpenAsync();
        //    }

        //    return conn;
        //}

        public void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }
    }
}
