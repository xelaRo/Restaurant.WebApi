using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Infrastructure.OracleDb
{
    public interface ICrudOperations
    {
        Task<IEnumerable<T>> GetList<T>(T entity, string query, object param = null);
        Task<T> SingleOrDefault<T>(T entity,string query, object param = null);
        Task<IEnumerable<dynamic>> GetListDynamic(string query);
        Task<int?> ExecuteAction(string query);
    }
}