using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Order
{
    public interface IFirstDbOrderRepository
    {
        Task Create(CreateNewOrderViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task<IEnumerable<Bill>> Get();
        Task Update(EditOrderViewModel editOrderViewModel);
    }
}