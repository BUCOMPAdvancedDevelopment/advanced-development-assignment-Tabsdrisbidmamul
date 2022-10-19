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
    public sealed class Create
    {
        public class Command: IRequest
        {
          public Game Game {get; set;}
        }

    public sealed class Handler : IRequestHandler<Command>
    {
      private readonly ILogger<Unit> _logger;
      private readonly DataContext _context;
      public Handler(DataContext context, ILogger<Unit> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        try
        {
        _context.Games.Add(request.Game);

        await _context.SaveChangesAsync();

        return Unit.Value;
        }
        catch(Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back");
          return Unit.Value;
        }

      }
    }
  }
}