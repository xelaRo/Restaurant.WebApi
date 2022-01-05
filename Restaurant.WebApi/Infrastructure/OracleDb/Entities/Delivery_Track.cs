using System;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Delivery_Track
    {
        public int Id { get; set; }
        public DateTime? PickedupAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public int CustomerId { get; set; }
        public int BillId { get; set; }
    }
}
