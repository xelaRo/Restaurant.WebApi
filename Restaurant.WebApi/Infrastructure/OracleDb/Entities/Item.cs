namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool Cooking { get; set; }
        public string Sku { get; set; }
        public float Price { get; set; }
        public int VendorId { get; set; }
    }
}
