namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int CountyId { get; set; }
        public string Name { get; set; }
    }
}
