namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class Delivery
    {
        public int Id { get; set; }
        public int PickedupDuration { get; set; }
        public int ReturnDuration { get; set; }
        public int DeliveryDuration { get; set; }
    }
}
