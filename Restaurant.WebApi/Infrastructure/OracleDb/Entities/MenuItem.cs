namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int ItemId { get; set; }
        public bool Active { get; set; }
    }
}
