using Restaurant.WebApi.Infrastructure.OracleDb.Entities;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Repository
{
    public interface ICustomerRepository
    {
        Task<ApplicationUser> GetUserByUserNameAndPassword(string username, string password);
    }
}