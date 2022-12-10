using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Services.Interfaces;

namespace Application.Images
{
  /// <summary>
  /// Helper class to remove media from Cloudinry no-sql table
  /// 
  /// We get the public id from the token claims that is sent up with every request
  /// </summary>
  public class Delete
  {
    public class Command: IRequest<Result<Unit>>
    {
      
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
      private readonly DataContext _context;
      private readonly ILogger<Result<Unit>> _logger;
      private readonly IUserNameAccessor _userNameAccessor;
      private readonly ICloudinaryPhoto _cloudinaryPhotoService;

      public Handler(DataContext context, ILogger<Result<Unit>> logger, IUserNameAccessor userNameAccessor, ICloudinaryPhoto cloudinaryPhoto)
      {
        _logger = logger;
        _context = context;
        _userNameAccessor = userNameAccessor;
        _cloudinaryPhotoService = cloudinaryPhoto;
      }

      public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
      {
        try 
        {
          var user = await _context.Users.Include(u => u.Image).FirstOrDefaultAsync(x => x.UserName == _userNameAccessor.GetUserName());

          if(user == null) return null;

          var deletionResult = await _cloudinaryPhotoService.DeletePhoto(user.Image.PublicId);

          if(deletionResult == null) return Result<Unit>.Failure("Error deleting user image");

          _context.ProfileImages.Remove(user.Image);
          user.Image = null;
          
          var results = await _context.SaveChangesAsync() > 0;

          if(!results) return Result<Unit>.Failure("Failed to delete user image");

          return Result<Unit>.Success(Unit.Value);

        } 
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
          return Result<Unit>.Failure("Something went wrong");
        } 
      }
    }
  }
}