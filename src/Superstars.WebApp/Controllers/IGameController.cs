using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    interface IGameController
    {

        Task<IActionResult> CreateGame();

        Task<IActionResult> GetGameDate();

    }
}
