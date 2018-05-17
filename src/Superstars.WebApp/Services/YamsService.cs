using System.Collections.Generic;
using System.Threading.Tasks;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class YamsService
    {
        readonly YamsGateway _yamsGateway;

        public YamsService(YamsGateway yamsGateway)
        {
            _yamsGateway = yamsGateway;
        }

        public int[] RollDices(int[] mydices, int[] selectedDices = null)
        {
            mydices = _yamsGateway.IndexChange(mydices, selectedDices);
            mydices = _yamsGateway.Reroll(mydices);
            return mydices;
        }

        public Task<Result<int>> CreateYamsPlayer(int gameid, int nbturn, string dices, int dicesvalue)
        {
            return _yamsGateway.CreateYamsPlayer(gameid, nbturn, dices, dicesvalue);
        }
        /*public async Task<GameData> FindYamsGameById(int GameID)
        {
            GameData game = await _yamsGateway.FindGameById(GameID);
            if (game != null)
            {
                return game;
            }
            return null;
        }*/
    }
}