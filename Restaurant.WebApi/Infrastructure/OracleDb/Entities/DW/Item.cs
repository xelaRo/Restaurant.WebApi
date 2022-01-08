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
        public int Menu1 { get; set; }
        public int Menu2 { get; set; }
        public int Menu3 { get; set; }
        public int Menu4 { get; set; }
        public int Menu5 { get; set; }
        public int Menu6 { get; set; }
        public int Menu7 { get; set; }
    }
}
