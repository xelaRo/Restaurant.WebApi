namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Cooking { get; set; }
        public string Sku { get; set; }
        public float CurrentPrice { get; set; }
        public string MenuName { get; set; }
    }
}
