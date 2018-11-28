using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.YamsFair;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ProvablyFairController : Controller
    {
        private readonly ProvablyFairGateway _provablyFairGateway;
        private readonly UserGateway _userGateway;

        public ProvablyFairController(ProvablyFairGateway provablyFairGateway, UserGateway userGateway)
        {
            _provablyFairGateway = provablyFairGateway;
            _userGateway = userGateway;
        }


        [HttpPost("CreateSeeds")]
        public async void CreateSeeds()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _provablyFairGateway.AddSeeds(userId);
        }


        [HttpGet("GetSeeds")]
        public async Task<ProvablyFairData> GetSeeds()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _provablyFairGateway.GetSeeds(userId);
            return result;
        }

        [HttpPost("UpdateSeeds")]
        public async void UpdateSeeds([FromBody] string ClientSeed = null)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData data = await _userGateway.FindById(userId);
            Result res = await _provablyFairGateway.UpdateSeeds(userId, ClientSeed);
            var seedData = await GetSeeds();
            Result actionRes = await _provablyFairGateway.ActionChangeSeeds(data.UserId, data.UserName, DateTime.UtcNow, seedData.ClientSeed, seedData.PreviousClientSeed, seedData.UncryptedServerSeed, seedData.UncryptedPreviousServerSeed);
        }

        [HttpPost("{clientSeedTest}/{serverSeedTest}/{nbOfDices}/RetriveDicesFromSeeds")]
        public int[] RetriveDicesFromSeeds(string clientSeedTest, string serverSeedTest, int nbOfDices)
        {
            var dicesFromSeeds = HashManager.RetriveDicesFromSeeds(clientSeedTest, serverSeedTest, nbOfDices);
            var dicesFromSeedsArray = dicesFromSeeds.ToArray();
            return dicesFromSeedsArray;
        }
    }
}