using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Games
{
    public class Edit
    {
      public class Command: IRequest
      {
        public Game Game { get; set; }
      }

      public class Handler : IRequestHandler<Command>
      {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
          _mapper = mapper;
          _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
          var game = await _context.Games.FindAsync(request.Game.Id);

          _mapper.Map(request.Game, game);

          await _context.SaveChangesAsync();

          return Unit.Value;
        }
      }
  }
}