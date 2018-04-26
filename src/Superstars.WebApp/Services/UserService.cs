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

        public Task<Result<int>> CreateUser(string email, string pseudo, string password)
        {
            return _userGateway.CreateUser(email, pseudo, _passwordHasher.HashPassword(password));
        }

        public async Task<UserData> FindUser(string pseudo, string password)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            if (user != null && _passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }

        public async Task<Result<int>> IdentityVerify(string pseudo, string password) 
        {
            return _userGateway.IdentityVerify(pseudo,_passwordHasher.HashPassword(password));
        }
    }
}