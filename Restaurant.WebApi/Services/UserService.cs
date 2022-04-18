using Restaurant.WebApi.Security;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Services
{
    public class UserService //: IUserService
    {
        //private IJwtFactory _JwtFactory;
        //private readonly Repository.ICustomerRepository _customerRepository;


        //public UserService(IJwtFactory jwtFactory, Repository.ICustomerRepository customerRepository)
        //{
        //    _JwtFactory = jwtFactory;
        //    _customerRepository = customerRepository;
        //}

        //public async Task<Token> AuthenticateUser(string username, string password)
        //{
        //    var user = await _customerRepository.GetUserByUserNameAndPassword(username, password);
        //    var token = await _JwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, user.UserRole);

        //    return token;
        //}
    }
}
