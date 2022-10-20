using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
    public class Delete
    {
      public class Command: IRequest
      {
        public Guid Id { get; set; }
    }

      public class Handler : IRequestHandler<Command>
      {
        private readonly DataContext _context;
        private readonly ILogger<Unit> _logger;
        public Handler(DataContext context, ILogger<Unit> logger)
        {
          _logger = logger;
          _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
          try 
          {
            var gameToRemove = await _context.Games.FindAsync(request.Id);
            _context.Remove(gameToRemove);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
          }
          catch (Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back");
            return Unit.Value;
          }

        }
      }
    }
}