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
      public class Command: IRequest<Result<Game>>
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

      public class Handler : IRequestHandler<Command, Result<Game>>
      {
        private readonly DataContext _context;
        private readonly ILogger<Result<Game>> _logger;
        public Handler(DataContext context, ILogger<Result<Game>> logger)
        {
          _context = context;
          _logger = logger;
        }

        public async Task<Result<Game>> Handle(Command request, CancellationToken cancellationToken)
        {
          try 
          {
            var game = await _context.Games.FindAsync(request.Game.Id);

            if(game == null) return Result<Game>.Failure("Failed to edit, game not found");

            game.Title = request.Game.Title;
            game.Description = request.Game.Description;
            game.Category  = request.Game.Category as List<string>;
            game.Price = request.Game.Price;
            game.CoverArt = request.Game.CoverArt as List<CoverArt>;
            game.YoutubeLink = request.Game.YoutubeLink;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if(!result) return Result<Game>.Failure("Failed to edit game");

            return Result<Game>.Success(game);

          }
          catch (Exception ex) when (ex is TaskCanceledException)
          {
            _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
            return Result<Game>.Failure("Something went wrong");
          }
          

        }
      }
  }
}