using System.Collections.Generic;

namespace Restaurant.WebApi.Models
{
    public class CreateNewOrderViewModel
    {
        public int CustomerId { get; set; }
        public bool IsDelivery { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
}
