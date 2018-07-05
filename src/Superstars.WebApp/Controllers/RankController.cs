﻿using Microsoft.AspNetCore.Authorization;
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

		[HttpGet("PlayersProfitSorted")]
		public async Task<IEnumerable<int>> GetPlayersProfitSorted()
		{
			IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
			IEnumerable<string> names = await _rankGateway.PseudoList();
			List<int> profitList = profits.ToList();
			List<string> namesList = names.ToList(); 
			_rankService.TriProfitAndRank(profitList,namesList);
			return profitList;
		}

		[HttpGet("PlayersUserNameSorted")]
		public async Task<IEnumerable<string>> GetPlayersUserNameSorted()
		{
			IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
			IEnumerable<string> names = await _rankGateway.PseudoList();
			List<int> profitList = profits.ToList();
			List<string> namesList = names.ToList();
			_rankService.TriProfitAndRank(profitList, namesList);
			return namesList;
		}

		[HttpGet("PlayersYamsNumberParts")]
		public async Task<IEnumerable<int>> GetPlayersYamsNumberParts()
		{
			IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
			IEnumerable<int> winsYams = await _rankGateway.GetPlayersYamsWins();
			IEnumerable<int> lossesYams = await _rankGateway.GetPlayerYamsLosses();
			List<int> profitList = profits.ToList();
			List<int> winsList = winsYams.ToList();
			List<int> lossesList = lossesYams.ToList();
			List<int> NbGame = _rankService.SortedNbGames(profitList,winsList,lossesList);
			return NbGame;
		}

		[HttpGet("PlayersBlackJackNumberParts")]
		public async Task<IEnumerable<int>> GetPlayersBlackJackNumberParts()
		{
			IEnumerable<int> profits = await _rankGateway.GetPlayerProfitList();
			IEnumerable<int> winsBlackJack = await _rankGateway.GetPlayersBlackJackWins();
			IEnumerable<int> lossesBlackJack = await _rankGateway.GetPlayersBlackJackLosses();
			List<int> profitList = profits.ToList();
			List<int> winsList = winsBlackJack.ToList();
			List<int> lossesList = lossesBlackJack.ToList();
			List<int> NbGame = _rankService.SortedNbGames(profitList, winsList, lossesList);
			return NbGame;
		}
	}
}
