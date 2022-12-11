using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Services.Interfaces;

namespace Application.Images
{
  /// <summary>
  /// Helper class to add a new image to a user
  /// 
  /// Will take the form and path, and push this onto cloudinary service to create an entry for the new image
  /// </summary>
  public class Add
  {
    public class Command: IRequest<Result<ProfileImage>> 
    {
      public IFormFile File { get; set; }
      public string Path { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<ProfileImage>>
    {
      private readonly DataContext _context;
      private readonly ICloudinaryPhoto _cloudinaryAccessor;
      private readonly IUserNameAccessor _userAccessor;
      private readonly ILogger<Result<ProfileImage>> _logger;

      public Handler(DataContext context, ICloudinaryPhoto photoAccessor, IUserNameAccessor userAccessor, ILogger<Result<ProfileImage>> logger)
      {
        _userAccessor = userAccessor;
        _cloudinaryAccessor = photoAccessor;
        _context = context;
        _logger = logger;

      }

      public async Task<Result<ProfileImage>> Handle(Command request, CancellationToken cancellationToken)
      {
        try
        {
          var user = await _context.Users.Include(u => u.Image)
          .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

          if(user == null) return null;

          var photoUploadResult = await _cloudinaryAccessor.AddPhoto(request.File, request.Path);

          var image = new ProfileImage
          {
            Url = photoUploadResult.Url,
            PublicId = photoUploadResult.PublicId
          };

          user.Image = image;

          var result = await _context.SaveChangesAsync() > 0;

          if(result) return Result<ProfileImage>.Success(image);

          return Result<ProfileImage>.Failure("Problem adding photo");

        } 
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Trace {ex.StackTrace?.ToString()}");

          return Result<ProfileImage>.Failure("Something went wrong");
        }
      }
    }
  }
}