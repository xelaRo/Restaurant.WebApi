using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Location
{
    public class FirstDbLocationRepository : IFirstDbLocationRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;

        public FirstDbLocationRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbCon = _dbConnection.GetConnection().Result;
        }

        public async Task Create(LocationViewModel locationViewModel)
        {
            var locationMaxIdScript = "SELECT MAX(Id) + 1 FROM Address_Location";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var location = await _dbCon.QueryFirstOrDefaultAsync<int>(locationMaxIdScript);

                    var billInsertScript = "INSERT INTO address_location (ID, CityId, Address)" +
                                     " SELECT " +
                                     $"{location}," +
                                     $"{locationViewModel.CityId}," +
                                     $"'{locationViewModel.Address}'" +
                                     " FROM dual";

                    var billInsertResult = await _dbCon.ExecuteAsync(billInsertScript);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

            }
        }


        public async Task Delete(int id)
        {
            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var billScript = $"SELECT * FROM Address_Location Where Id = {id}";
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<AddressLocation>(billScript);

                    var billDeleteScript = $"DELETE FROM Address_Location Where Id = {bill.Id}";
                    var billDeleteScriptresult = await _dbCon.ExecuteAsync(billDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<AddressLocation>> Get()
        {
            try
            {
                var billDictionary = new Dictionary<int, Bill>();

                var script =
                    "SELECT * FROM Address_Location";

                var con = await _dbConnection.GetConnection();
                var result = await con.QueryAsync<AddressLocation>(script);

                _dbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(LocationEditViewModel editViewModel)
        {
            var billScript = $"SELECT * FROM Address_Location WHERE Id = {editViewModel.Id}";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<AddressLocation>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The location with id {editViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Address_Location " +
                        $"SET CITYID = '{editViewModel.CityId}', " +
                        $" ADDRESS = '{editViewModel.Address}'" +
                        $" WHERE id = {bill.Id}";

                    var result = await _dbCon.ExecuteAsync(billUpdateScript);

                    if (result > 0)
                    {
                        tran.Commit();
                    }
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}

