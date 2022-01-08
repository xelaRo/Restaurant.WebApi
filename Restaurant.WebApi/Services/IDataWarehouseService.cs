using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public interface IDataWarehouseService
    {
        Task<IEnumerable<Sales>> GetAllSales();
    }
}