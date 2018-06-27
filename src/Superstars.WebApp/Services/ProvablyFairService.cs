using Superstars.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class ProvablyFairService
    {
        readonly ProvablyFairGateway _provablyFairGateway;

        public ProvablyFairService(ProvablyFairGateway provablyFairGateway, PasswordHasher passwordHasher)
        {
            _provablyFairGateway = provablyFairGateway;
        }


        public async Task<ProvablyFairData> FindSeeds(int userId)
        {
            ProvablyFairData provablyFair = await _provablyFairGateway.GetSeeds(userId);
            if (provablyFair != null)
            {
                return provablyFair;
            }
            return null;
        }
    }
}
