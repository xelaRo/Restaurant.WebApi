using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Item
{
    public interface IFirstDbItemRepository
    {
        Task Create(Models.SecondDb.ItemSecondDbViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Item>> Get();
        Task Update(Models.SecondDb.ItemEditViewModel editOrderViewModel);
    }
}