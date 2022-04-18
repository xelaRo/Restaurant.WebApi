using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.SecondDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Item
{
    public class FirstDbItemRepository : IFirstDbItemRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;
        public FirstDbItemRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbCon = _dbConnection.GetConnection().Result;
        }

        public async Task Create(Models.SecondDb.ItemSecondDbViewModel itemViewModel)
        {
            var billMaxIdScript = "SELECT MAX(Id) + 1 FROM Item";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var billMaxId = await _dbCon.QueryFirstOrDefaultAsync<int>(billMaxIdScript);

                    var billInsertScript = "INSERT INTO Item (ID, Title, Summary, Cooking, Sku, Price, VendorId)" +
                                     " SELECT " +
                                     $"{billMaxId}," +
                                     $"'{itemViewModel.Title}'," +
                                     $"'{itemViewModel.Summary}'," +
                                     $"{itemViewModel.Cooking}," +
                                     $"'{itemViewModel.Sku}'," +
                                     $"{itemViewModel.Price}," +
                                     $"{itemViewModel.VendorId}" +
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
                    var billScript = $"SELECT * FROM Item Where Id = {id}";
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Item>(billScript);

                    var billDeleteScript = $"DELETE FROM Item Where Id = {bill.Id}";
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

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Item>> Get()
        {
            try
            {
                var script = "SELECT * FROM Item";

                var con = await _dbConnection.GetConnection();
                var result = await con.QueryAsync<Infrastructure.OracleDb.Entities.Item>(script);

                _dbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(ItemEditViewModel vendorEditViewModel)
        {
            var billScript = $"SELECT * FROM Item WHERE Id = {vendorEditViewModel.Id}";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var bill = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Item>(billScript);

                    if (bill == null)
                    {
                        throw new ArgumentNullException($"The Item with id {vendorEditViewModel.Id} was not found");
                    }

                    var billUpdateScript = $"UPDATE Item " +
                        $"SET Title = '{vendorEditViewModel.Title}'," +
                        $" VendorId = '{vendorEditViewModel.VendorId}'" +
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
