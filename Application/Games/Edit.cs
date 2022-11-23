using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Games.Validator;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
    public class Edit
    {
      public class Command: IRequest<Result<Unit>>
      {
        public Game Game { get; set; }
      }

      public class CommandValidator : AbstractValidator<Command>
      {
        public CommandValidator()
        {
          RuleFor(x => x.Game).SetValidator(new GameValidator());
        }
      }

      public class Handler : IRequestHandler<Command, Result<Unit>>
      {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
         private readonly ILogger<Result<Unit>> _logger;
        public Handler(DataContext context, IMapper mapper, ILogger<Result<Unit>> logger)
        {
          _mapper = mapper;
          _context = context;
          _logger = logger;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
          try 
          {
            var game = await _context.Games.FindAsync(request.Game.Id);

            if(game == null) return Result<Unit>.Failure("Failed to edit, game not found");

            _mapper.Map(request.Game, game);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if(!result) return Result<Unit>.Failure("Failed to edit game");

            return Result<Unit>.Success(Unit.Value);

          }
          catch (Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.InnerException}");
            return Result<Unit>.Failure("Something went wrong");
          }
          

        }
      }
  }
}