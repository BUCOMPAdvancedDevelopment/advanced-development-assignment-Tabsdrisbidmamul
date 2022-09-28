using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Games
{
    public sealed class List
    {
        public sealed class Query: IRequest<List<Game>>
        {
        }

    public sealed class Handler : IRequestHandler<Query, List<Game>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<List<Game>> Handle(Query request, CancellationToken cancellationToken)
      {
        return await _context.Games.ToListAsync();
      }
    }
  }
}