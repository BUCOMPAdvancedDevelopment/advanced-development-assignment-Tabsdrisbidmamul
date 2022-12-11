using Application.Games;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{ 
  /// <summary>
  /// Games endpoints
  /// </summary>
  public sealed class GamesController : BaseApiController
  {
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetGames(CancellationToken cancellationToken)
    {
      return HandleResult(await Mediator.Send(new Application.Games.List.Query(), cancellationToken));
    }

    /// <summary>
    /// Get a single game from the id
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="id">game id</param>
    /// <returns>HTTP status code and result</returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(CancellationToken cancellationToken, Guid id)
    {
       return HandleResult(await Mediator.Send(new Application.Games.Single.Query { Id = id }, cancellationToken));
    }

    /// <summary>
    /// Get all game categories, only works if the user is an admin user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>categories as string list</returns>
    [Authorize(Policy = "IsAdmin")]
    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
      return HandleResult(await Mediator.Send(new Application.Games.ListTypes.Query(), cancellationToken));
    }
    
    /// <summary>
    /// Create a new game into the game table, only works if the user is an admin user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="game">game</param>
    /// <returns>HTTP status code and result</returns>
    [Authorize(Policy = "IsAdmin")]
    [HttpPost]
    public async Task<IActionResult> CreateGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return HandleResult(await Mediator
        .Send(new Application.Games.Create.Command { Game = game }, cancellationToken));
    }
    
    /// <summary>
    /// Edit a game in the game table, only works if the user is an admin user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="game">game</param>
    /// <returns>HTTP status code and result</returns>
    [Authorize(Policy = "IsAdmin")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> EditGame(CancellationToken cancellationToken, [FromBody]Game game)
    {
      return HandleResult(await Mediator.Send(new Application.Games.Edit.Command { Game = game }, cancellationToken));
    }

    /// <summary>
    /// Delete a game from the game table, only works if the user is an admin user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="id">game id</param>
    /// <returns>HTTP status code</returns>
    [Authorize(Policy = "IsAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(CancellationToken cancellationToken, string id)
    {
      return HandleResult(await Mediator.Send(new Application.Games.Delete.Command { Id = new Guid(id) }, cancellationToken));
    }
    
  }
}