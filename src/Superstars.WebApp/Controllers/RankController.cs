using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;
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
		RankService _rankService;

		public RankController(RankGateway rankGateway,RankService rankService)
		{
			_rankGateway = rankGateway;
			_rankService = rankService;
		}

		[HttpGet("{TrueOrFake}/PlayersProfitSorted")]
		public async Task<IEnumerable<int>> GetPlayersProfitSorted(bool TrueOrFake)
		{
			IEnumerable<string> names = await _rankGateway.PseudoList();
			List<int> profitList = new List<int>();
			List<string> namesList = names.ToList(); 
			if(TrueOrFake)
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerTrueProfitList();
				profitList = profits.ToList(); 
			}
			else
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
				profitList = profits.ToList();
			}
			_rankService.TriProfitAndRank(profitList, namesList);
			return profitList;
		}

		[HttpGet("{TrueOrFake}/PlayersUserNameSorted")]
		public async Task<IEnumerable<string>> GetPlayersUserNameSorted(bool TrueorFake)
		{
			IEnumerable<string> names = await _rankGateway.PseudoList();
			List<int> profitList = new List<int>();
			if (TrueorFake)
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerTrueProfitList();
				profitList = profits.ToList();
			}
			else
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
				profitList = profits.ToList();
			}
			List<string> namesList = names.ToList();
			_rankService.TriProfitAndRank(profitList, namesList);
			return namesList;
		}

		[HttpGet("{TrueOrFake}/PlayersYamsNumberParts")]
		public async Task<IEnumerable<int>> GetPlayersYamsNumberParts(bool TrueOrFake)
		{
			IEnumerable<int> winsYams = await _rankGateway.GetPlayersYamsWins();
			IEnumerable<int> lossesYams = await _rankGateway.GetPlayerYamsLosses();
			List<int> profitList = new List<int>();
			List<int> winsList = winsYams.ToList();
			List<int> lossesList = lossesYams.ToList();
			if(TrueOrFake)
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerTrueProfitList();
				profitList = profits.ToList();
			}
			else
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
				profitList = profits.ToList();
			}
			List<int> NbGame = _rankService.SortedNbGames(profitList,winsList,lossesList);
			return NbGame;
		}

		[HttpGet("{TrueOrFake}/PlayersBlackJackNumberParts")]
		public async Task<IEnumerable<int>> GetPlayersBlackJackNumberParts(bool TrueOrFake)
		{
			IEnumerable<int> winsBlackJack = await _rankGateway.GetPlayersBlackJackWins();
			IEnumerable<int> lossesBlackJack = await _rankGateway.GetPlayersBlackJackLosses();
			List<int> profitList = new List<int>();
			List<int> winsList = winsBlackJack.ToList();
			List<int> lossesList = lossesBlackJack.ToList();
			if(TrueOrFake)
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerTrueProfitList();
				profitList = profits.ToList();
			}
			else
			{
				IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
				profitList = profits.ToList();
			}
			List<int> NbGame = _rankService.SortedNbGames(profitList, winsList, lossesList);
			return NbGame;
		}

    }
}
