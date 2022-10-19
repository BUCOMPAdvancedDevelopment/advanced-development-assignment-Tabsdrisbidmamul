using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
    public class Single
    {
        public class Query: IRequest<Game>
        {
          public Guid Id { get; set; }
        }

    public class Handler : IRequestHandler<Query, Game>
    {
      private readonly DataContext _context;
      private readonly ILogger<Game> _logger;
      public Handler(DataContext context, ILogger<Game> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Game> Handle(Query request, CancellationToken cancellationToken)
      {
        try 
        {
          return await _context.Games.FindAsync(request.Id);
        }
        catch(Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back");
          return new Game();
        }
      }
    }

  }
}