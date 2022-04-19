

using Dapper;
using Microsoft.Extensions.Configuration;
using Restaurant.WebApi.Infrastructure.OracleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Menu
{
    public class FirstDbMenuItemRepository : IFirstDbMenuItemRepository
    {
        private DbConnection _dbConnection;
        private IDbConnection _dbCon;

        public FirstDbMenuItemRepository(IConfiguration configuration)
        {
            _dbConnection = new DbConnection(configuration.GetConnectionString("FirstDB"));
            _dbCon = _dbConnection.GetConnection().Result;
        }

        public async Task<IEnumerable<Infrastructure.OracleDb.Entities.Menu>> GetMenusAndItems()
        {
            try
            {
                var menuDictionary = new Dictionary<int, Infrastructure.OracleDb.Entities.Menu>();

                var script = "SELECT * FROM Menu m" +
                    " JOIN Menu_Item mi on mi.menuid = m.Id" +
                    " JOIN Item i on i.Id = mi.ItemId";

                var result = await _dbCon.QueryAsync<Infrastructure.OracleDb.Entities.Menu, Infrastructure.OracleDb.Entities.MenuItem, Infrastructure.OracleDb.Entities.Item, Infrastructure.OracleDb.Entities.Menu>(
                        script,
                        (menu, menuItem, item) =>
                        {
                            Infrastructure.OracleDb.Entities.Menu menuEntry;

                            if (!menuDictionary.TryGetValue(menu.Id, out menuEntry))
                            {
                                menuEntry = menu;
                                menu.Items = new List<Infrastructure.OracleDb.Entities.Item>();
                                menuDictionary.Add(menuEntry.Id, menuEntry);
                            }

                            menuEntry.Items.Add(item);

                            return menuEntry;
                        }
                    );

                _dbConnection.CloseConnection(_dbCon);

                return menuDictionary.Select(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
