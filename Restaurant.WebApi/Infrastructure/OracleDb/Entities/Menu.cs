using System.Collections.Generic;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<Item> Items { get; set; }
    }
}
