using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.GlobalDb.Vendor
{
    public interface IGlobalDbVendorRepository
    {
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get();
    }
}