using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Menu
{
    public interface IFirstDbMenuItemRepository
    {
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Menu>> GetMenusAndItems();
    }
}