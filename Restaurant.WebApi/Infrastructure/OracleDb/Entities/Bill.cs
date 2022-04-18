using System;
using System.Collections.Generic;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public float ShippingCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Order_Item> OrderItems { get; set; }
        public Delivery_Track? DeliveryTrack { get; set; }
    }
}
