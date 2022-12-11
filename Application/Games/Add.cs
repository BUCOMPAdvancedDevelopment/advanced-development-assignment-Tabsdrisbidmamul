using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Services.Interfaces;

namespace Application.Games
{
  /// <summary>
  /// Helper class to add media to a game - requires for the game to exist in the db first
  /// 
  /// 
  /// </summary>
  public class Add
  {
    public class Command: IRequest<Result<CoverArt>> 
    {
      public IFormFile File { get; set; }
      public string Id { get; set; }
      public string IsBoxArt { get; set; }
      public string Path { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<CoverArt>>
    {
      private readonly DataContext _context;
      private readonly ICloudinaryPhoto _cloudinaryAccessor;
      private readonly ILogger<Result<ProfileImage>> _logger;

      public Handler(DataContext context, ICloudinaryPhoto photoAccessor, IUserNameAccessor userAccessor, ILogger<Result<ProfileImage>> logger)
      {
        _cloudinaryAccessor = photoAccessor;
        _context = context;
        _logger = logger;

      }

      public async Task<Result<CoverArt>> Handle(Command request, CancellationToken cancellationToken)
      {
        try
        {
          var game = await _context.Games
            .Include(p => p.CoverArt)
            .FirstOrDefaultAsync(g => g.Id == new Guid(request.Id));

          if(game == null) return null;

          var photoUploadResult = await _cloudinaryAccessor.AddPhoto(request.File, request.Path);

          var coverImage = new CoverArt
          {
            PublicId = photoUploadResult.PublicId,
            Url = photoUploadResult.Url,
            IsBoxArt = bool.Parse(request.IsBoxArt)
          };

          if(game.CoverArt == null) 
          {
            game.CoverArt = new List<CoverArt>
            {
              coverImage
            };
          } else {
            var image = game.CoverArt.FirstOrDefault(x => x.IsBoxArt == bool.Parse(request.IsBoxArt));

            if(image != null) {
              // remove images from db - they are still referenced even if you change the images for said game - the GET will still return the stale images
              _context.CoverArt.Remove(image);

              game.CoverArt.Remove(image);        
            }

            game.CoverArt.Add(coverImage);
          }

          var result = await _context.SaveChangesAsync() > 0;

          if(result) return Result<CoverArt>.Success(coverImage);

          return Result<CoverArt>.Failure("Problem adding photo");

        } 
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace {ex.StackTrace?.ToString()}");

          return Result<CoverArt>.Failure("Something went wrong");
        }
      }
    }
  }
}