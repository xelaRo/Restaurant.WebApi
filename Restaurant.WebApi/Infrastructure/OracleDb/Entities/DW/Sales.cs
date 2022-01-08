using System.Collections.Generic;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class Sales
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TimeId { get; set; }
        public int AddressLocationId { get; set; }
        public int BillId { get; set; }
        public int DeliveryId { get; set; }
        public int Quantity { get; set; }
        public float SeelingPrice { get; set; }

        public Bill Bill { get; set; }
        public Customer Customer { get; set; }
        public Delivery Delivery { get; set; }
        public Item Item { get; set; }
        public DwTime DwTime { get; set; }
        public LocationAddress LocationAddress { get; set; }
    }
}
