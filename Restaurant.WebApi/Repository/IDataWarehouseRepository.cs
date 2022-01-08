using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public interface IDataWarehouseRepository
    {
        Task<IEnumerable<Sales>> GetAllSales();
    }
}