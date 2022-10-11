using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
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
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        _context.Games.Add(request.Game);

        await _context.SaveChangesAsync();

        return Unit.Value;
      }
    }
  }
}