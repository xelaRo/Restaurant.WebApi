using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public interface IDataWarehouseService
    {
        Task<IEnumerable<Sales>> GetAllSales();
        Task<string> GetItemSalesForLast10Years();
        Task<string> GetItemTotalSalesOnMenu();
        Task<string> GetTotalItemSalesBetweenThisYearAndLastYear();
        Task<string> GetAvergeDeliveryTimeOnCountyAndQuarter();
        Task<string> GetSalesForItemsThatRequireCooking();
    }
}