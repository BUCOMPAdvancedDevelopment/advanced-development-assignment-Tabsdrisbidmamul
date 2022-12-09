using Application.Core;
using Application.Games.Validator;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
    public sealed class Create
    {
      public class Command: IRequest<Result<Game>>
      {
        public Game Game {get; set;}
      }

      public class CommandValidator : AbstractValidator<Command>
      {
        public CommandValidator()
        {
          RuleFor(x => x.Game).SetValidator(new GameValidator());
        }
      }

      public sealed class Handler : IRequestHandler<Command, Result<Game>>
      {
        private readonly ILogger<Result<Game>> _logger;
        private readonly DataContext _context;
        public Handler(DataContext context, ILogger<Result<Game>> logger)
        {
          _logger = logger;
          _context = context;
        }

        public async Task<Result<Game>> Handle(Command request, CancellationToken cancellationToken)
        {
          try
          {
            _context.Games.Add(request.Game);

            var result = await _context.SaveChangesAsync(cancellationToken)  > 0;

            if(!result) return Result<Game>.Failure("Failed to create game");

            var game = await _context.Games.FindAsync(request.Game.Id);

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