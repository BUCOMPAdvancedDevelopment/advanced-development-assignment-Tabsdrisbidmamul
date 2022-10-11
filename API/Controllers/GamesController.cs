using Application.Games;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public sealed class GamesController : BaseApiController
  {

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetGames()
    {
      return await Mediator.Send(new Application.Games.List.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(Guid id)
    {
      return await Mediator.Send(new Application.Games.Single.Query { Id = id });
    }

  }
}