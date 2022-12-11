using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    /// <summary>
    /// Search endpoints
    /// </summary>
    [AllowAnonymous]
    public class SearchController: BaseApiController
    {
      /// <summary>
      /// Search all games in the game db against the query
      /// </summary>
      /// <param name="cancellationToken"></param>
      /// <param name="query">query</param>
      /// <returns>found games</returns>
      [HttpGet]
      [Route("search-games/{query}")]
      public async Task<IActionResult> SearchGame(CancellationToken cancellationToken, string query) 
      {
        return HandleResult(await Mediator.Send(new Application.Games.Search.Query { Term = query }, cancellationToken));
      }
    }
}