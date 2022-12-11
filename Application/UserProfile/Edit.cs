using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Models;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using Services.Interfaces;

namespace Application.UserProfile
{
  /// <summary>
  /// Helper class to edit User profiles
  /// 
  /// Transform request object to be added to user profile record
  /// </summary>
  public class Edit
  {
    public class Command: IRequest<Result<ProfileEdit>>
    {
      public string DisplayName { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
      {
        public CommandValidator()
        {
          RuleFor(x => x.DisplayName).NotEmpty();
        }
      }

    public class Handler : IRequestHandler<Command, Result<ProfileEdit>>
    {
      private readonly DataContext _context;
      private readonly IUserNameAccessor _userAccessor;
      private readonly ILogger<Result<ProfileEdit>> _logger;
      public Handler(DataContext context, ILogger<Result<ProfileEdit>> logger, IUserNameAccessor userNameAccessor)
      {
        _context = context;
        _logger = logger;
        _userAccessor = userNameAccessor;
      }

      public async Task<Result<ProfileEdit>> Handle(Command request, CancellationToken cancellationToken)
      {
        try 
        {
          var user = await _context.Users.Include(u => u.Image).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

          if(user == null) return null;

          user.DisplayName = request.DisplayName;

          var result = await _context.SaveChangesAsync() > 0;

          if(!result) return Result<ProfileEdit>.Failure("Error updating profile");

          return Result<ProfileEdit>.Success(new ProfileEdit {DisplayName = request.DisplayName});
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
          return Result<ProfileEdit>.Failure("Something went wrong");
        }
        

      }
    }
  }
}