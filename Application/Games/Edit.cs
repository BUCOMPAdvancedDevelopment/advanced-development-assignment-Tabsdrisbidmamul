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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
    public class Edit
    {
      public class Command: IRequest<Result<List<Game>>>
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

      public class Handler : IRequestHandler<Command, Result<List<Game>>>
      {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
         private readonly ILogger<Result<List<Game>>> _logger;
        public Handler(DataContext context, IMapper mapper, ILogger<Result<List<Game>>> logger)
        {
          _mapper = mapper;
          _context = context;
          _logger = logger;
        }

        public async Task<Result<List<Game>>> Handle(Command request, CancellationToken cancellationToken)
        {
          try 
          {
            var game = await _context.Games.FindAsync(request.Game.Id);

            if(game == null) return Result<List<Game>>.Failure("Failed to edit, game not found");

            _mapper.Map(request.Game, game);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if(!result) return Result<List<Game>>.Failure("Failed to edit game");

            var allGames = await _context.Games.Include(p => p.CoverArt).ToListAsync();

            return Result<List<Game>>.Success(allGames);

          }
          catch (Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
            return Result<List<Game>>.Failure("Something went wrong");
          }
          

        }
      }
  }
}