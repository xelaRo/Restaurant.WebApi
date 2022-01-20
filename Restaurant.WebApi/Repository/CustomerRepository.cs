using Dapper;
using Restaurant.WebApi.Infrastructure.OracleDb;
using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using System;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IRestaurantDbConnection _dbCon;
        private readonly ICrudOperations _crudOperations;

        public CustomerRepository(IRestaurantDbConnection dbCon, ICrudOperations crudOperations)
        {
            _dbCon = dbCon;
            _crudOperations = crudOperations;
        }

        public async Task<ApplicationUser> GetUserByUserNameAndPassword(string username, string password)
        {
            try
            {
                var user = await _crudOperations.SingleOrDefault(new ApplicationUser(), $"Where UserName = '{username}' AND UserPassword = '{password}'",null);

                if(user == null)
                {
                    throw new ArgumentException($"The user with {username} was not found.");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
