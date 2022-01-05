namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class AddressLocation
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
    }
}
