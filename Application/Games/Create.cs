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
      public class Command: IRequest<Result<Unit>>
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

      public sealed class Handler : IRequestHandler<Command, Result<Unit>>
      {
        private readonly ILogger<Result<Unit>> _logger;
        private readonly DataContext _context;
        public Handler(DataContext context, ILogger<Result<Unit>> logger)
        {
          _logger = logger;
          _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
          try
          {
            _context.Games.Add(request.Game);

            var result = await _context.SaveChangesAsync(cancellationToken)  > 0;

            if(!result) return Result<Unit>.Failure("Failed to create game");

            return Result<Unit>.Success(Unit.Value);

          }
          catch(Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace {ex.InnerException}");
            return Result<Unit>.Failure("Something went wrong");
          }
          
        }
      }
  }
}