namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VendorId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
    }
}
