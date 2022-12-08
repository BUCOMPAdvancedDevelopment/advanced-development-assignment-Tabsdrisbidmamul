using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
  public class Delete
  {
    public class Command: IRequest<Result<List<Game>>>
    {
      public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<List<Game>>>
    {
      private readonly DataContext _context;
      private readonly ILogger<Result<List<Game>>> _logger;
      public Handler(DataContext context, ILogger<Result<List<Game>>> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Result<List<Game>>> Handle(Command request, CancellationToken cancellationToken)
      {
        try 
        {
          var gameToRemove = await _context.Games.FindAsync(request.Id);

          if(gameToRemove == null) return null;

          _context.Remove(gameToRemove);

          var result = await _context.SaveChangesAsync(cancellationToken) > 0;

          if(!result) return Result<List<Game>>.Failure("Failed to delete game");

          var allGames = await _context.Games.Include(g => g.CoverArt).ToListAsync();

          return Result<List<Game>>.Success(allGames);

        } 
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
          return Result<List<Game>>.Failure("Something went wrong");
        }
        
      }
    }
  }
}