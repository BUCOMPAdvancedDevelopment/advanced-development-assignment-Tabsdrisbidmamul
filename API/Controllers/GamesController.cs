using Application.Games;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public sealed class GamesController : BaseApiController
  {

    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetGames(CancellationToken cancellationToken)
    {
      return HandleResult(await Mediator.Send(new Application.Games.List.Query(), cancellationToken));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(CancellationToken cancellationToken, Guid id)
    {
       return HandleResult(await Mediator.Send(new Application.Games.Single.Query { Id = id }, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return HandleResult(await Mediator
        .Send(new Application.Games.Create.Command { Game = game }, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return HandleResult(await Mediator.Send(new Application.Games.Edit.Command { Game = game }, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(CancellationToken cancellationToken, Guid id)
    {
      return HandleResult(await Mediator.Send(new Application.Games.Delete.Command { Id = id }, cancellationToken));
    }
    
  }
}