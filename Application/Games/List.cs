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
  public sealed class List
  {
    public sealed class Query: IRequest<Result<List<Game>>>
    {
    }

    public sealed class Handler : IRequestHandler<Query, Result<List<Game>>>
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
          var result = await _context.Games
            .Include(g => g.CoverArt)
            .ToListAsync(cancellationToken);

          return Result<List<Game>>.Success(result);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace: {ex.StackTrace?.ToString()}");
          return Result<List<Game>>.Failure("Something went wrong");
        }
      }
    }
  }
}