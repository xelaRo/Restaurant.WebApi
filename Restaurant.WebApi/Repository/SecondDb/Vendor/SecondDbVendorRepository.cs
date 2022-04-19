using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.SecondDb.Vendor
{
    public class SecondDbVendorRepository : ISecondDbVendorRepository
    {
        private DbConnection _secondDbConnection;
        private IDbConnection _secondDbCon;
        public SecondDbVendorRepository(IConfiguration configuration)
        {
            _secondDbConnection = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _secondDbCon = _secondDbConnection.GetConnection().Result;
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get()
        {
            try
            {
                var script = "SELECT * FROM Vendor";

                var result = await _secondDbCon.QueryAsync<Infrastructure.OracleDb.Entities.Vendor>(script);

                _secondDbConnection.CloseConnection(_secondDbCon);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
