using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Vendor
{
    public interface IFirstDbVendorRepository
    {
        Task Create(VendorViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get();
        Task Update(VendorEditViewModel editOrderViewModel);
    }
}