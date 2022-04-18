using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using Restaurant.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository.FirstDb.Customer
{
    public interface IFirstDbCustomerRepository
    {
        Task Create(CustomerViewModel customerViewModel);
        Task Delete(int id);
        Task<IEnumerable<Infrastructure.OracleDb.Entities.Customer>> Get();
        Task Update(CustomerEditViewModel editOrderViewModel);
    }
}