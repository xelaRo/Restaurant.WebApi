namespace Restaurant.WebApi.Models.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public bool Cooking { get; set; }
        public string Sku { get; set; }
        public float Price { get; set; }
    }
}