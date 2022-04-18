using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Customer
{
    public class FirstDbCustomerRepository : IFirstDbCustomerRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;
        public FirstDbCustomerRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbCon = _dbConnection.GetConnection().Result;
        }

        public async Task Create(CustomerViewModel customerViewModel)
        {
            var customerMaxScript = "SELECT MAX(Id) + 1 FROM Customer";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var customerMaxId = await _dbCon.QueryFirstOrDefaultAsync<int>(customerMaxScript);

                    var customerInsertScript = "INSERT INTO Customer (ID, FIRSTNAME, LASTNAME, EMAIL,PHONE,GENDER,ADDRESSID,USERID)" +
                                     " SELECT " +
                                     $"{customerMaxId}," +
                                     $"'{customerViewModel.FirstName}'," +
                                     $"'{customerViewModel.LastName}'," +
                                     $"'{customerViewModel.Email}'," +
                                     $"'{customerViewModel.Phone}'," +
                                     $"'{customerViewModel.Gender}'," +
                                     $"{customerViewModel.AddressId}," +
                                     $"{customerViewModel.UserId}" + 
                                     " FROM dual";

                    var customerInsertResult = await _dbCon.ExecuteAsync(customerInsertScript);
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
                    var customerScript = $"SELECT * FROM Customer Where Id = {id}";
                    var customer = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Customer>(customerScript);

                    if(customer is null)
                    {
                        throw new Exception();
                    }

                    var customerDeleteScript = $"DELETE FROM Customer Where Id = {customer.Id}";
                    await _dbCon.ExecuteAsync(customerDeleteScript);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Customer>> Get()
        {
            try
            {
                var script =
                    "SELECT * FROM Customer";

                var con = await _dbConnection.GetConnection();
                var result = await con.QueryAsync<Infrastructure.OracleDb.Entities.Customer>(script);

                _dbConnection.CloseConnection(con);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(CustomerEditViewModel customerEditViewModel)
        {
            var customerScript = $"SELECT * FROM Customer WHERE Id = {customerEditViewModel.CustomerId}";

            using (var tran = _dbCon.BeginTransaction())
            {
                try
                {
                    var customer = await _dbCon.QueryFirstOrDefaultAsync<Infrastructure.OracleDb.Entities.Customer>(customerScript);

                    if (customer == null)
                    {
                        throw new ArgumentNullException($"The customer with id {customerEditViewModel.CustomerId} was not found");
                    }

                    var billUpdateScript = $"UPDATE Customer " +
                        $"SET FirstName = '{customerEditViewModel.FirstName}'," +
                        $" LastName = '{customerEditViewModel.LastName}'," +
                        $" Email = '{customerEditViewModel.Email}'," +
                        $" Phone = '{customerEditViewModel.Phone}'" +
                        $" WHERE id = {customer.Id}";

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
