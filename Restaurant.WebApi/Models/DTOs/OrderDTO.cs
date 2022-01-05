using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public float ShippingCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
