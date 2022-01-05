using System.Threading.Tasks;

namespace Restaurant.WebApi.Security
{
    public interface IJwtFactory
    {
        Task<Token> GenerateEncodedToken(string id, string userName, string role);
    }
}