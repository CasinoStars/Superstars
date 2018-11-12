using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Superstars.DAL;
using Superstars.WebApp.Authentication;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ChatController : Controller
    {
        private readonly ChatGateway _chatGateway;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly ILogger _logger;

        public ChatController(ChatGateway chatGateway, IHubContext<SignalRHub> hubContext, ILogger<ChatController> logger)
        {
            _chatGateway = chatGateway;
            _hubContext = hubContext;
            _logger = logger;
        }
        [HttpPost("{message}")]
        public async Task<IActionResult> CreateMessage(string message)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var pseudo = User.FindFirst(ClaimTypes.Name).Value;
            await _hubContext.Clients.All.SendAsync("Message", pseudo, message);
            Result result = await _chatGateway.CreateMessage(userId, message);
            return this.CreateResult(result);
        }
        [HttpGet("getMessages")]
        public async Task<IActionResult> GetMessageList()
        {
            var list = await _chatGateway.ListAll();
            _logger.LogWarning(list.First().TextMessage);
            return Ok(list);
        }
    }
}
