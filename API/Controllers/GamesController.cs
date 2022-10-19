using Application.Games;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public sealed class GamesController : BaseApiController
  {

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames(CancellationToken cancellationToken)
    {
      return await Mediator.Send(new Application.Games.List.Query(), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(CancellationToken cancellationToken, Guid id)
    {
      return await Mediator.Send(new Application.Games.Single.Query { Id = id }, cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return StatusCode(StatusCodes.Status201Created, await Mediator
        .Send(new Application.Games.Create.Command {Game = game}, cancellationToken));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> EditGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return StatusCode(StatusCodes.Status200OK, await Mediator.Send(new Application.Games.Edit.Command { Game = game }, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(CancellationToken cancellationToken, Guid id)
    {
      return StatusCode(StatusCodes.Status204NoContent, await Mediator.Send(new Application.Games.Delete.Command { Id = id }, cancellationToken));
    }
    
  }
}