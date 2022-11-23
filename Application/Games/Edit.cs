using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      public class Command: IRequest
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

      public class Handler : IRequestHandler<Command>
      {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
         private readonly ILogger<Unit> _logger;
        public Handler(DataContext context, IMapper mapper, ILogger<Unit> logger)
        {
          _mapper = mapper;
          _context = context;
        _logger = logger;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
          try 
          {
            var game = await _context.Games.FindAsync(request.Game.Id);

            _mapper.Map(request.Game, game);

            await _context.SaveChangesAsync(cancellationToken);

          }
          catch (Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back");
          }
          return Unit.Value;

        }
      }
  }
}