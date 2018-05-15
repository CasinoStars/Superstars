using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models.AccountViewModels;
using Superstars.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace Superstars.WebApp.Controllers
{
    public class GameController : controller
    {
        readonly GameService _gameservice;

        public GameController(GameService gameservice) {
            _gameservice = gameservice;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateGame() {
        if (ModelState.IsValid)
            {
                Result result = await _gameService.CreateGame(model.GameType);
                if (result.HasError)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(model);
                }
                GameData game = await _gameService.FindGameById(model.GameID);
            }
            return View(model);

        }
    }
}
