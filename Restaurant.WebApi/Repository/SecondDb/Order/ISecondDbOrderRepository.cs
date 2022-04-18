using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.SecondDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.SecondDb.Order
{
    public interface ISecondDbOrderRepository
    {
        Task Create(OrderSecondDbViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task<IEnumerable<Bill>> Get();
        Task Update(EditOrderViewModel editOrderViewModel);
    }
}