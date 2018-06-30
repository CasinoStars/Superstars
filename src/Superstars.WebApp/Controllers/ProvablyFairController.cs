using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.Wallet;
using NBitcoin;
using QBitNinja.Client;
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


        [HttpGet("GetSeeds")]
        public async Task<ProvablyFairData> GetSeeds()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ProvablyFairData result = await _provablyFairGateway.GetSeeds(userId);
            return result;
        }

        [HttpGet("UpdateSeeds")]
        public async void UpdateSeeds()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _provablyFairGateway.UpdateSeeds(userId);
        }
    }
}