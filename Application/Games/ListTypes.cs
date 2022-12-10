using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain.Types;
using Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Games
{
  /// <summary>
  /// Helper class to aggregate all category types and return them as a string array
  /// </summary>
  public sealed class ListTypes
  {
    public sealed class Query: IRequest<Result<List<string>>>
    {
    }

    public sealed class Handler : IRequestHandler<Query, Result<List<string>>>
    {
      private readonly DataContext _context;
      private readonly ILogger<Result<List<string>>> _logger;

      public Handler(DataContext context, ILogger<Result<List<string>>> logger)
      {
        _logger = logger;
        _context = context;
      }

      public async Task<Result<List<string>>> Handle(Query request, CancellationToken cancellationToken)
      {
        try 
        {
          var allCategoryValues = Enum.GetValues(typeof(CategoryTypes))
            .Cast<CategoryTypes>()
            .Select(t => t.GetStringValue())
            .ToList();

          return Result<List<string>>.Success(allCategoryValues);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace: {ex.StackTrace?.ToString()}");
          return Result<List<string>>.Failure("Something went wrong");
        }
      }
    }
  }
}