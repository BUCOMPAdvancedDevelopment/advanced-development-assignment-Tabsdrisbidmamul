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
    /// Helper class to return back a single game from the db
    /// </summary>
    public sealed class Single
    {
        public class Query: IRequest<Result<Game>>
        {
          public Guid Id { get; set; }
        }

    public class Handler : IRequestHandler<Query, Result<Game>>
    {
      private readonly DataContext _context;
      private readonly ILogger<Result<Game>> _logger;
      public Handler(DataContext context, ILogger<Result<Game>> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Result<Game>> Handle(Query request, CancellationToken cancellationToken)
      {
        try 
        {
          var game = await _context.Games
            .Include(g => g.CoverArt)
            .FirstOrDefaultAsync<Game>(x => x.Id == request.Id);

          return Result<Game>.Success(game);
        }
        catch(Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace {ex.StackTrace?.ToString()}");

          return Result<Game>.Failure("Something went wrong");
        }
      }
    }

  }
}