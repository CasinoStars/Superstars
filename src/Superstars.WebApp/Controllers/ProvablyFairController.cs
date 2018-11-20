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

        public ProvablyFairController(ProvablyFairGateway provablyFairGateway)
        {
            _provablyFairGateway = provablyFairGateway;
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
        public void UpdateSeeds([FromBody] string ClientSeed = null)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _provablyFairGateway.UpdateSeeds(userId, ClientSeed);
        }

        [HttpPost("{clientSeedTest}/{serverSeedTest}/{nbOfDices}/RetriveDicesFromSeeds")]
        public int[] RetriveDicesFromSeeds(string clientSeedTest, string serverSeedTest, int nbOfDices)
        {
            var dicesFromSeeds = HashManager.RetriveDicesFromSeeds(clientSeedTest, serverSeedTest, nbOfDices,6);
            var dicesFromSeedsArray = dicesFromSeeds.ToArray();
            return dicesFromSeedsArray;
        }
    }
}