using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Vendor
{
    public class GlobalDbVendorRepository : IGlobalDbVendorRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;

        public GlobalDbVendorRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("RestaurantGlobal"));
            _dbCon = _dbConnection.GetConnection().Result;
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get()
        {
            try
            {
                var script = "SELECT * FROM Vendor";

                var con = await _dbConnection.GetConnection();
                var result = await con.QueryAsync<Infrastructure.OracleDb.Entities.Vendor>(script);

                _dbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
