using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Vendor
{
    public class FirstDbVendorRepository : IFirstDbVendorRepository
    {
        private DbConnection _firstDbConnection;
        private DbConnection _secondDbConnection;
        private IDbConnection _firstDbCon;
        private IDbConnection _secondDbCon;
        public FirstDbVendorRepository(IConfiguration configuration)
        {
            _firstDbConnection = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _secondDbConnection = new DbConnection(configuration.GetConnectionString("SecondDB"));
            _firstDbCon = _firstDbConnection.GetConnection().Result;
            _secondDbCon = _secondDbConnection.GetConnection().Result;
        }

        public async Task Create(VendorViewModel vendorViewModel)
        {
            var firstVendorMaxIdScript = "SELECT MAX(Id) + 1 FROM Vendor";

            using (var tran = _firstDbCon.BeginTransaction())
            {
                try
                {
                    var vendor = await _firstDbCon.QueryFirstOrDefaultAsync<int>(firstVendorMaxIdScript);

                    var billInsertScript = "INSERT INTO Vendor (ID, Name)" +
                                     " SELECT " +
                                     $"{vendor}," +
                                     $"'{vendorViewModel.Name}'" +
                                     " FROM dual";

                    var billInsertResult = await _firstDbCon.ExecuteAsync(billInsertScript);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            using (var tran = _secondDbCon.BeginTransaction())
            {
                try
                {
                    var billMaxId = await _secondDbCon.QueryFirstOrDefaultAsync<int>(firstVendorMaxIdScript);

                    var billInsertScript = "INSERT INTO Vendor (ID, Email, Phone)" +
                                     " SELECT " +
                                     $"{billMaxId}," +
                                     $"'{vendorViewModel.Email}'," +
                                     $"'{vendorViewModel.Phone}'" +
                                     " FROM dual";

                    var billInsertResult = await _secondDbCon.ExecuteAsync(billInsertScript);

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
            using (var tran = _firstDbCon.BeginTransaction())
            {
                try
                {
                    var billScript = $"SELECT * FROM Vendor Where Id = {id}";
                    var bill = await _firstDbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    var billDeleteScript = $"DELETE FROM Vendor Where Id = {bill.Id}";
                    var billDeleteScriptresult = await _firstDbCon.ExecuteAsync(billDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }

            using (var tran = _secondDbCon.BeginTransaction())
            {
                try
                {
                    var billScript = $"SELECT * FROM Vendor Where Id = {id}";
                    var bill = await _secondDbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    var billDeleteScript = $"DELETE FROM Vendor Where Id = {bill.Id}";
                    var billDeleteScriptresult = await _secondDbCon.ExecuteAsync(billDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get()
        {
            try
            {
                var script = "SELECT * FROM Vendor";

                var con = await _firstDbConnection.GetConnection();
                var result = await con.QueryAsync<Infrastructure.OracleDb.Entities.Vendor>(script);

                _firstDbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(VendorEditViewModel vendorEditViewModel)
        {
            var billScript = $"SELECT * FROM Vendor WHERE Id = {vendorEditViewModel.Id}";

            using (var tran = _firstDbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _firstDbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Vendor with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Vendor " +
                        $"SET Name = '{vendorEditViewModel.Name}'" +
                        $"WHERE id = {bill.Id}";

                    var result = await _firstDbCon.ExecuteAsync(billUpdateScript);

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

            using (var tran = _secondDbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _secondDbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Vendor>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Vendor with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Vendor " +
                        $"SET Email = '{vendorEditViewModel.Email}'," +
                        $" Phone = '{vendorEditViewModel.Phone}'" +
                        $"WHERE id = {bill.Id}";

                    var result = await _secondDbCon.ExecuteAsync(billUpdateScript);

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
