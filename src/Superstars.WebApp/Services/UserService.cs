using System;
using System.Threading.Tasks;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class UserService
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly UserGateway _userGateway;

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public Task<Result> CreateUser(string pseudo, string password, string email, string privateKey, int isAdmin)
        {
            return _userGateway.CreateUser(pseudo, _passwordHasher.HashPassword(password), email, privateKey, isAdmin);
        }

        public async Task<UserData> FindUser(string pseudo, string password)
        {
            var user = await _userGateway.FindByName(pseudo);
            if (user != null && _passwordHasher.VerifyHashedPassword(user.UserPassword, password) ==
                PasswordVerificationResult.Success) return user;
            return null;
        }

        public async Task<Result> UpdatePassword(int userId, string password)
        {
            return await _userGateway.UpdatePassword(userId, _passwordHasher.HashPassword(password));
        }

        public async Task<Result> UpdateMail(int userId, string mail)
        {
            return await _userGateway.UpdateEmail(userId, mail);
        }

        public async Task<UserData> FindByUserId(int userId)
        {
            return await _userGateway.FindById(userId);
        }

        public async Task ActionDeconnexion(int userId, string userName, DateTime date)
        {
            await _userGateway.ActionDeconnexion(userId, userName, date);
        }

        public async Task ActionConnexion(int userId, string userName, DateTime date)
        {
            await _userGateway.ActionConnexion(userId, userName, date);
        }

        public async Task ActionChangeEmail(int userId, string userName, DateTime date, string email)
        {
            await _userGateway.ActionChangeEmail(userId, userName, date, email);
        }

        public async Task ActionChangePassword(int userId, string userName, DateTime date, string password)
        {
            await _userGateway.ActionChangePaswword(userId, userName, date, password);
        }
    }
}