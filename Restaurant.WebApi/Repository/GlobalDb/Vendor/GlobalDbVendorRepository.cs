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
        private DbConnection _dbSecondDb;
        private DbConnection _dbConnectionFirstDb;
        private DbConnection _dbConnectionGlobalDb;
        private IDbConnection _dbConSecondDb;
        private IDbConnection _dbConFirstDb;
        private IDbConnection _dbConGlobalDb;

        public GlobalDbVendorRepository(IConfiguration configuration)
        {
            _dbConnectionFirstDb = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbConFirstDb = _dbConnectionFirstDb.GetConnection().Result;

            _dbSecondDb = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _dbConSecondDb = _dbSecondDb.GetConnection().Result;

            _dbConnectionGlobalDb = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _dbConGlobalDb = _dbConnectionGlobalDb.GetConnection().Result;
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get()
        {
            try
            {
                var script = "SELECT * FROM Vendor";

                var result = await _dbConGlobalDb.QueryAsync<Infrastructure.OracleDb.Entities.Vendor>(script);

                _dbConGlobalDb.Close();

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

            using (var tran = _dbConFirstDb.BeginTransaction())
            {
                try
                {
                    var vendor = await _dbConFirstDb.QueryFirstOrDefaultAsync<int>(firstVendorMaxIdScript);

                    var billInsertScript = "INSERT INTO Vendor (ID, Name)" +
                                     " SELECT " +
                                     $"{vendor}," +
                                     $"'{vendorViewModel.Name}'" +
                                     " FROM dual";

                    var billInsertResult = await _dbConFirstDb.ExecuteAsync(billInsertScript);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            using (var tran = _dbConSecondDb.BeginTransaction())
            {
                try
                {
                    var vendor = await _dbConSecondDb.QueryFirstOrDefaultAsync<int>(firstVendorMaxIdScript);

                    var billInsertScript = "INSERT INTO Vendor (ID, Email, Phone)" +
                                     " SELECT " +
                                     $"{vendor}," +
                                     $"'{vendorViewModel.Email}'," +
                                     $"'{vendorViewModel.Phone}'" +
                                     " FROM dual";

                    var billInsertResult = await _dbConSecondDb.ExecuteAsync(billInsertScript);

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
            using (var tran = _dbConFirstDb.BeginTransaction())
            {
                try
                {
                    var vendorScript = $"SELECT * FROM Vendor Where Id = {id}";
                    var vendor = await _dbConFirstDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(vendorScript);

                    var billDeleteScript = $"DELETE FROM Vendor Where Id = {vendor.Id}";
                    var billDeleteScriptresult = await _dbConFirstDb.ExecuteAsync(billDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }

            using (var tran = _dbConSecondDb.BeginTransaction())
            {
                try
                {
                    var vendorScript = $"SELECT * FROM Vendor Where Id = {id}";
                    var vendor = await _dbConSecondDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(vendorScript);

                    var billDeleteScript = $"DELETE FROM Vendor Where Id = {vendor.Id}";
                    var billDeleteScriptresult = await _dbConSecondDb.ExecuteAsync(billDeleteScript);

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

            using (var tran = _dbConFirstDb.BeginTransaction())
            {
                try
                {
                    var bill = await _dbConFirstDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Vendor with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Vendor " +
                        $"SET Name = '{vendorEditViewModel.Name}'" +
                        $"WHERE id = {bill.Id}";

                    var result = await _dbConFirstDb.ExecuteAsync(billUpdateScript);

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

            using (var tran = _dbConSecondDb.BeginTransaction())
            {
                try
                {
                    var bill = await _dbConSecondDb.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Vendor with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Vendor " +
                        $"SET Email = '{vendorEditViewModel.Email}'," +
                        $" Phone = '{vendorEditViewModel.Phone}'" +
                        $"WHERE id = {bill.Id}";

                    var result = await _dbConSecondDb.ExecuteAsync(billUpdateScript);

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
