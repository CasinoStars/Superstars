using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class RankController : Controller
    {
        private readonly RankGateway _rankGateway;
        private readonly RankService _rankService;

        public RankController(RankGateway rankGateway, RankService rankService)
        {
            _rankGateway = rankGateway;
            _rankService = rankService;
        }

        [HttpGet("{moneyTypeId}/GetPlayersGlobalProfit")]
        public async Task<IEnumerable<RankData>> GetPlayersGlobalProfit(int moneytypeId)
        {
            return (List<RankData>) await _rankGateway.GetPlayersGlobalProfit(moneytypeId);
        }

        [HttpGet("{pseudo}/{moneyTypeId}/GetPlayerStats")]
        public async Task<IEnumerable<RankData>> GetPlayerStats(string pseudo, int moneytypeId)
        {
            return (List<RankData>) await _rankGateway.GetPlayerStats(pseudo, moneytypeId);
        }

        [HttpGet("{TrueOrFake}/PlayersProfitSorted")]
        [AllowAnonymous]
        public async Task<IEnumerable<int>> GetPlayersProfitSorted(bool TrueOrFake)
        {
            var names = await _rankGateway.PseudoList();
            var profitList = new List<int>();
            var namesList = names.ToList();
            if (TrueOrFake)
            {
                var profits = await _rankGateway.GetPlayerTrueProfitList();
                profitList = profits.ToList();
            }
            else
            {
                var profits = await _rankGateway.GetPlayerProfitList();
                profitList = profits.ToList();
            }

            _rankService.TriProfitAndRank(profitList, namesList);
            return profitList;
        }

        [HttpGet("{TrueOrFake}/PlayersUserNameSorted")]
        [AllowAnonymous]
        public async Task<IEnumerable<string>> GetPlayersUserNameSorted(bool TrueorFake)
        {
            var names = await _rankGateway.PseudoList();
            var profitList = new List<int>();
            if (TrueorFake)
            {
                var profits = await _rankGateway.GetPlayerTrueProfitList();
                profitList = profits.ToList();
            }
            else
            {
                var profits = await _rankGateway.GetPlayerProfitList();
                profitList = profits.ToList();
            }

            var namesList = names.ToList();
            _rankService.TriProfitAndRank(profitList, namesList);
            return namesList;
        }

        [HttpGet("{TrueOrFake}/PlayersYamsNumberParts")]
        [AllowAnonymous]
        public async Task<IEnumerable<int>> GetPlayersYamsNumberParts(bool TrueOrFake)
        {
            var winsYams = await _rankGateway.GetPlayersYamsWins();
            var lossesYams = await _rankGateway.GetPlayerYamsLosses();
            var profitList = new List<int>();
            var winsList = winsYams.ToList();
            var lossesList = lossesYams.ToList();
            if (TrueOrFake)
            {
                var profits = await _rankGateway.GetPlayerTrueProfitList();
                profitList = profits.ToList();
            }
            else
            {
                var profits = await _rankGateway.GetPlayerProfitList();
                profitList = profits.ToList();
            }

            var NbGame = _rankService.SortedNbGames(profitList, winsList, lossesList);
            return NbGame;
        }

        [HttpGet("{TrueOrFake}/PlayersBlackJackNumberParts")]
        [AllowAnonymous]
        public async Task<IEnumerable<int>> GetPlayersBlackJackNumberParts(bool TrueOrFake)
        {
            var winsBlackJack = await _rankGateway.GetPlayersBlackJackWins();
            var lossesBlackJack = await _rankGateway.GetPlayersBlackJackLosses();
            var profitList = new List<int>();
            var winsList = winsBlackJack.ToList();
            var lossesList = lossesBlackJack.ToList();
            if (TrueOrFake)
            {
                var profits = await _rankGateway.GetPlayerTrueProfitList();
                profitList = profits.ToList();
            }
            else
            {
                var profits = await _rankGateway.GetPlayerProfitList();
                profitList = profits.ToList();
            }

            var NbGame = _rankService.SortedNbGames(profitList, winsList, lossesList);
            return NbGame;
        }
    }
}