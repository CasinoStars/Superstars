using System.Threading.Tasks;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class GameService
    {
        //readonly UserGateway _userGateway;
        readonly GameGateway _gamegateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(GameGateway gameGateway, PasswordHasher passwordHasher)
        {
            _gamegateway = gameGateway;
            _passwordHasher = passwordHasher;
        }

        public Task<Result> CreateGame(GamesTypes type )
        {
            return _gamegateway.CreateGame(type);
        }

        public async Task<GameData> FindGameById(int GameID)
        {
            GameData game = await _gameGateway.FindGameById(GameID);
            if (game != null)
            {
                return game;
            }
            return null;
        }
    }
}