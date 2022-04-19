using Restaurant.WebApi.Models;
using Restaurant.WebApi.Models.SecondDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Item
{
    public interface IGlobalDbItemRepository
    {
        Task Create(Models.SecondDb.ItemSecondDbViewModel itemViewModel);
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Item>> Get();
        Task Delete(int id);
        Task Update(ItemEditViewModel itemEditViewModel);
    }
}