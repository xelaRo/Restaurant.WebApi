namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Order_Item
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public Item Item { get; set; }
    }
}
