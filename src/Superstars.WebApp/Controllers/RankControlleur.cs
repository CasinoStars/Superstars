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
	[Route("api/[rank]")]
	[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
	public class RankControlleur : Controller
	{
		// RankGateway _rankGateway;
		UserData _userData;
		RankGateway _rankGateway;

		public RankControlleur(UserData userData)
		{
			_userData = userData;
		}

		[HttpGet("GetPseudoList")]
		public async Task<List<string>> GetPseudoList()
		{
			List<string> names = await _rankGateway.PseudoList();
			return names;
		}

		
	}
}
