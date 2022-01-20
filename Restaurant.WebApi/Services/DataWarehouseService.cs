using Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW;
using Restaurant.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public class DataWarehouseService : IDataWarehouseService
    {
        private readonly IDataWarehouseRepository dataWarehouseRepository;

        public DataWarehouseService(IDataWarehouseRepository dataWarehouseRepository)
        {
            this.dataWarehouseRepository = dataWarehouseRepository;
        }

        public async Task<IEnumerable<Sales>> GetAllSales()
        {
            return await dataWarehouseRepository.GetAllSales();
        }

        public async Task<string> GetItemSalesForLast10Years()
        {
             return await dataWarehouseRepository.GetItemSalesForLast10Years();
        }

        public async Task<string> GetItemTotalSalesOnMenu()
        {
            return await dataWarehouseRepository.GetItemTotalSalesOnMenu();
        }

        public async Task<string> GetTotalItemSalesBetweenThisYearAndLastYear()
        {
            return await dataWarehouseRepository.GetTotalItemSalesBetweenThisYearAndLastYear();
        }

        public async Task<string> GetAvergeDeliveryTimeOnCountyAndQuarter()
        {
            return await dataWarehouseRepository.GetAvergeDeliveryTimeOnCountyAndQuarter();
        }

        public async Task<string> GetSalesForItemsThatRequireCooking()
        {
            return await dataWarehouseRepository.GetSalesForItemsThatRequireCooking();
        }
    }
}
