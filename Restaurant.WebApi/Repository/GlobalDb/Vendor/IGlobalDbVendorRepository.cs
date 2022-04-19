using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Vendor
{
    public interface IGlobalDbVendorRepository
    {
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get();
        Task Create(VendorViewModel vendorViewModel);
        Task Delete(int id);
        Task Update(VendorEditViewModel vendorEditViewModel);
    }
}