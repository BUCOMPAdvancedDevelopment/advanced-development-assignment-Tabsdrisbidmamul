using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
  public sealed class List
  {
    public sealed class Query: IRequest<List<Game>>
    {
    }

    public sealed class Handler : IRequestHandler<Query, List<Game>>
    {
      private readonly DataContext _context;
      private readonly ILogger<List> _logger;
      public Handler(DataContext context, ILogger<List> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<List<Game>> Handle(Query request, CancellationToken cancellationToken)
      {
        try 
        {
          return await _context.Games.ToListAsync(cancellationToken);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back");
          return null;
        }
      }
    }
  }
}