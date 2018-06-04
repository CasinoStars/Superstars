using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    public class BlackJackController : Controller
    {
        Deck _deck;

        BlackJackController()
        {
            _deck = new Deck();
            _deck.CreateDeck();
        }
    }
}
