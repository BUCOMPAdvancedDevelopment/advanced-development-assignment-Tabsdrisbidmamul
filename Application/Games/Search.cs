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

  /// <summary>
  /// Helper class to search terms against game search indices
  /// </summary>
  public sealed class Search
  {
    public class Query: IRequest<Result<List<Game>>>
    {
      public string Term { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<Game>>>
    {
      private readonly DataContext _context;
      private readonly ILogger<Result<List<Game>>> _logger;
      public Handler(DataContext context, ILogger<Result<List<Game>>> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Result<List<Game>>> Handle(Query request, CancellationToken cancellationToken)
      {
        try 
        {
          var games = await _context.Games.Include(p => p.CoverArt).Where(x => x.SearchVector.Matches(request.Term)).ToListAsync(cancellationToken);

          if(games == null && games.Count() == 0) 
          {
            return Result<List<Game>>.Failure("No games found matching search term");
          }

          return Result<List<Game>>.Success(games);
        }
        catch(Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace {ex.StackTrace?.ToString()}");

          return Result<List<Game>>.Failure("Something went wrong");
        }
      }
    }

  }
}