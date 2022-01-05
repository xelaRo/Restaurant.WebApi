using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Models.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public Item Item { get; set; }
    }
}
