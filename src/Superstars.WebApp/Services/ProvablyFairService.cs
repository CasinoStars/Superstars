using System.Threading.Tasks;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class ProvablyFairService
    {
        private readonly ProvablyFairGateway _provablyFairGateway;

        //public ProvablyFairService(ProvablyFairGateway provablyFairGateway, PasswordHasher passwordHasher)
        //{
        //    _provablyFairGateway = provablyFairGateway;
        //}


        public async Task<ProvablyFairData> FindSeeds(int userId)
        {
            var provablyFair = await _provablyFairGateway.GetSeeds(userId);
            if (provablyFair != null) return provablyFair;
            return null;
        }

        public async Task<int> GetDice(int userId)
        {
            var dice = await _provablyFairGateway.GetDicesFromHash(userId);
            return dice;
        }
    }
}