using System.Threading.Tasks;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class UserService
    {
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public Task<Result> CreateUser(string pseudo, string password, string email, string privateKey)
        {
            return _userGateway.CreateUser(pseudo, _passwordHasher.HashPassword(password), email, privateKey);
        }

        public async Task<UserData> FindUser(string pseudo, string password)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            if (user != null && _passwordHasher.VerifyHashedPassword(user.UserPassword, password) == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }
    }
}