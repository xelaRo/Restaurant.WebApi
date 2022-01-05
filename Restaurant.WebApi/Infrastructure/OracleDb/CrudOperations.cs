using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public class CrudOperations : ICrudOperations
    {
        private readonly IRestaurantDbConnection _dbCon;

        public CrudOperations(IRestaurantDbConnection dbCon)
        {
            _dbCon = dbCon;
        }

        public async Task<IEnumerable<T>> GetList<T>(T entity, string query, object param = null)
        {
            var connection = await _dbCon.GetOltpConnection();

            var columns = GetColumnList(entity, query);
            var result = await connection.QueryAsync<T>(columns, param);

            _dbCon.CloseConnection(connection);

            return result;
        }

        public async Task<T> SingleOrDefault<T>(T entity, string query, object param = null)
        {
            var connection = await _dbCon.GetOltpConnection();

            var result = await connection.QueryFirstOrDefaultAsync<T>(GetColumnList(entity, query), param);

            _dbCon.CloseConnection(connection);

            return result;
        }

        public async Task<IEnumerable<dynamic>> GetListDynamic(string query)
        {
            var connection = await _dbCon.GetOltpConnection();

            var result = await connection.QueryAsync(query);

            _dbCon.CloseConnection(connection);

            return result;
        }

        public async Task<int?> ExecuteAction(string query)
        {
            var connection = await _dbCon.GetOltpConnection();

            var result = await connection.ExecuteAsync(query);

            _dbCon.CloseConnection(connection);

            return result;
        }

        private static string GetColumnList<T>(T entity, string query)
        {
            string selectedColumns = "Select ";
            var lastProperty = entity.GetType().GetProperties().Last();

            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop == lastProperty)
                { 
                    selectedColumns = selectedColumns + prop.Name + " AS " + prop.Name;
                }
                else
                {
                    selectedColumns = selectedColumns + prop.Name + " AS " + prop.Name + ",";
                }
            }

            return selectedColumns + " From " + ConvertToPascalCase(entity.GetType().Name) + " " + query;
        }

        private static string ConvertToPascalCase(string str)
        {

            var upperChar = str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString());

            return string.Concat(upperChar);
            //var string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
