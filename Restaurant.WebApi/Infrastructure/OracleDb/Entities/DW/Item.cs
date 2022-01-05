namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool Cooking { get; set; }
        public string Sku { get; set; }
        public float CurrentPrice { get; set; }
        public bool Active { get; set; }
        public bool Menu1 { get; set; }
        public bool Menu2 { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public bool Menu5 { get; set; }
        public bool Menu6 { get; set; }
        public bool Menu7 { get; set; }
    }
}
