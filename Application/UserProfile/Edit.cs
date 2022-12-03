using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
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
  public class Edit
  {
    public class Command: IRequest<Result<string>>
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

    public class Handler : IRequestHandler<Command, Result<string>>
    {
      private readonly DataContext _context;
      private readonly IUserNameAccessor _userAccessor;
      private readonly ILogger<Result<string>> _logger;
      public Handler(DataContext context, ILogger<Result<string>> logger, IUserNameAccessor userNameAccessor)
      {
        _context = context;
        _logger = logger;
        _userAccessor = userNameAccessor;
      }

      public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
      {
        try 
        {
          var user = await _context.Users.Include(u => u.Image).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

          if(user == null) return null;

          user.DisplayName = request.DisplayName;

          var result = await _context.SaveChangesAsync() > 0;

          if(!result) return Result<string>.Failure("Error updating profile");

          return Result<string>.Success(request.DisplayName);
        }
        catch (Exception ex) when (ex is TaskCanceledException)
        {
          _logger.LogInformation($"ERROR: {this.GetType()} Task was cancelled, rolling back\nStack Tract {ex.StackTrace?.ToString()}");
          return Result<string>.Failure("Something went wrong");
        }
        

      }
    }
  }
}