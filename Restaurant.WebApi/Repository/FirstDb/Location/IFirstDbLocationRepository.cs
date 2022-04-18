using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Location
{
    public interface IFirstDbLocationRepository
    {
        Task Create(LocationViewModel createNewOrderViewModel);
        Task Delete(int id);
        Task<IEnumerable<AddressLocation>> Get();
        Task Update(LocationEditViewModel editViewModel);
    }
}