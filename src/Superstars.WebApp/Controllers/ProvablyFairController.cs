using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.YamsFair;
using System.Collections.Generic;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ProvablyFairController : Controller
    {
        ProvablyFairGateway _provablyFairGateway;

        public ProvablyFairController(ProvablyFairGateway provablyFairGateway)
        {
            _provablyFairGateway = provablyFairGateway;
        }


        [HttpPost("CreateSeeds")]
        public async void CreateSeeds()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _provablyFairGateway.AddSeeds(userId);

        }


        [HttpGet("GetSeeds")]
        public async Task<ProvablyFairData> GetSeeds()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ProvablyFairData result = await _provablyFairGateway.GetSeeds(userId);
            return result;
        }

        [HttpPost("UpdateSeeds")]
        public async void UpdateSeeds([FromBody] string ClientSeed = null)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _provablyFairGateway.UpdateSeeds(userId, ClientSeed);
        }
        [HttpPost("{clientSeedTest}/{serverSeedTest}/{nbOfDices}/RetriveDicesFromSeeds")]
        public async Task<int[]> RetriveDicesFromSeeds(string clientSeedTest ,string serverSeedTest,  int nbOfDices)
        {
             List<int> dicesFromSeeds =  HashManager.RetriveDicesFromSeeds(clientSeedTest, serverSeedTest, nbOfDices);
            int[] dicesFromSeedsArray = dicesFromSeeds.ToArray();
            return dicesFromSeedsArray;
        }
    }
}