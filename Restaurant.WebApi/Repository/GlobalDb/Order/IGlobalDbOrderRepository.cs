using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Order
{
    public interface IGlobalDbOrderRepository
    {
        Task<IEnumerable<Bill>> Get();
    }
}