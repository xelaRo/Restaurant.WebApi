using System;

namespace Restaurant.WebApi.Models.DTOs
{
    public class DeliveryTrackDTO
    {
        public int Id { get; set; }
        public DateTime PickedupAt { get; set; }
        public DateTime DelivedAt { get; set; }
        public DateTime ReturnedAt { get; set; }
    }
}
