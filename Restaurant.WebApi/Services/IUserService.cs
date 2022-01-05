using Restaurant.WebApi.Security;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public interface IUserService
    {
        Task<Token> AuthenticateUser(string username, string password);
    }
}