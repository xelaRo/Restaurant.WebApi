using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Bill>> GetOrdersWithDeliveryByCustomerId(int customerId);
        Task<IEnumerable<Bill>> GetAllOrders();
        Task<IEnumerable<Menu>> GetMenusAndItems();
        Task CreateOrder(int customerId, bool isDelivery, List<ItemViewModel> items);
        Task EditOrder(int billId, int orderStatusId);
    }
}