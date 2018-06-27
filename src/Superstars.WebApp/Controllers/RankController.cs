using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
	public class RankController : Controller
	{
		RankGateway _rankGateway;

		public RankController(RankGateway rankGateway)
		{
			_rankGateway = rankGateway;
		}

		[HttpGet("PseudoList")]
		public async Task<IEnumerable<string>> GetPseudoList()
		{
			IEnumerable<string> names = await _rankGateway.PseudoList();
			return names;
		}

		[HttpGet("PlayerProfit")]
		public async Task<IEnumerable<int>> GetPlayerProfit(string pseudo)
		{
			IEnumerable<int> profit = await _rankGateway.GetPlayerProfit(pseudo);
			return profit;
		}
	}
}
