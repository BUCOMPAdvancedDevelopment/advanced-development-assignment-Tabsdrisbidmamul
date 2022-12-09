using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class SearchController: BaseApiController
    {
      [HttpGet]
      [Route("search-games/{query}")]
      public async Task<IActionResult> SearchGame(CancellationToken cancellationToken, string query) 
      {
        return HandleResult(await Mediator.Send(new Application.Games.Search.Query { Term = query }, cancellationToken));
      }
    }
}