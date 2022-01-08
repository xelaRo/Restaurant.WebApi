using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Bill>> GetOrdersWithDeliveryByCustomerId(int customerId);
        Task<IEnumerable<Bill>> GetAllOrders();
        Task<IEnumerable<Menu>> GetMenusAndItems();
        Task CreateNewOrder(CreateNewOrderViewModel createNewOrderViewModel);
        Task EditOrder(EditOrderViewModel editOrder);
    }
}