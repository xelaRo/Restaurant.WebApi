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
    }
}
