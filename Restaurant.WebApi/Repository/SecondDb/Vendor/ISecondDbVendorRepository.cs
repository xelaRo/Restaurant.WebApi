using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.SecondDb.Vendor
{
    public interface ISecondDbVendorRepository
    {
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Vendor>> Get();
    }
}