using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Models;
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

        public async Task Create(VendorViewModel vendorViewModel)
        {
            var firstVendorMaxIdScript = "SELECT MAX(Id) + 1 FROM Vendor";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var vendor = await _dbCon.QueryFirstOrDefaultAsync<int>(firstVendorMaxIdScript);

                    var billInsertScript = "INSERT INTO Vendor (ID, Name, Email, Phone)" +
                                     " SELECT " +
                                     $"{vendor}," +
                                     $"'{vendorViewModel.Name}'," +
                                     $"'{vendorViewModel.Email}'," +
                                     $"'{vendorViewModel.Phone}'" +
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
                    var vendorScript = $"SELECT * FROM Vendor Where Id = {id}";
                    var vendor = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(vendorScript);

                    var billDeleteScript = $"DELETE FROM Vendor Where Id = {vendor.Id}";
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

        public async Task Update(VendorEditViewModel vendorEditViewModel)
        {
            var billScript = $"SELECT * FROM Vendor WHERE Id = {vendorEditViewModel.Id}";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Vendor with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Vendor " +
                        $"SET Name = '{vendorEditViewModel.Name}'," +
                        $" Email = '{vendorEditViewModel.Email}'," +
                        $" Phone = '{vendorEditViewModel.Phone}'" +
                        $"WHERE id = {bill.Id}";

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
