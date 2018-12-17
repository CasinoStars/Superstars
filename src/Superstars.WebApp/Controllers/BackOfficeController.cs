using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class BackOfficeController : Controller
    {
        private readonly RankGateway _rankGateway;
        public BackOfficeController(RankGateway RankGateway)
        {
            _rankGateway = RankGateway;
        }

        //public async Task<IEnumerable<LogData>> GetLogsList()
        //{
        //    var table = 
        //}
    }
}
