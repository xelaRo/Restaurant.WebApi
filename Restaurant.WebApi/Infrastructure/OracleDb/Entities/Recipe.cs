namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int IngredientId { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
    }
}
