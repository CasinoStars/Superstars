using Superstars.DAL;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class ProvablyFairService
    {
        readonly ProvablyFairGateway _provablyFairGateway;

        //public ProvablyFairService(ProvablyFairGateway provablyFairGateway, PasswordHasher passwordHasher)
        //{
        //    _provablyFairGateway = provablyFairGateway;
        //}


        public async Task<ProvablyFairData> FindSeeds(int userId)
        {
            ProvablyFairData provablyFair = await _provablyFairGateway.GetSeeds(userId);
            if (provablyFair != null)
            {
                return provablyFair;
            }
            return null;
        }

        public async Task<int> GetDice(int userId)
        {
             int dice =  await _provablyFairGateway.GetDicesFromHash(userId);
            return dice;
        }
    }
}
