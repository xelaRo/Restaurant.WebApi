using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Order
{
    public interface IGlobalDbOrderRepository
    {
        Task<IEnumerable<Bill>> Get();
        Task Create(CreateNewOrderViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task Update(EditOrderViewModel editOrderViewModel);

    }
}