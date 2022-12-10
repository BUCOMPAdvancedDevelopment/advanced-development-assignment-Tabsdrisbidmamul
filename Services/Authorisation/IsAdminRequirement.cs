using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Types;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Services.Authorisation
{
    /// <summary>
    /// Claim based authorisation - we place a role into the JWT token, and we check if the user has the role in their db record, if so we will grant them access to role authorisation
    /// </summary>
    public class IsAdminRequirement: IAuthorizationRequirement
    {
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
      private readonly DataContext _dbContext;
      private readonly IHttpContextAccessor _httpContextAccessor;

      public IsAdminRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
      {
        _dbContext = dbContext;
      _httpContextAccessor = httpContextAccessor;
      }

      /// <summary>
      /// Grant user access if the user is an admin
      /// </summary>
      /// <param name="context"></param>
      /// <param name="requirement"></param>
      /// <returns></returns>
      protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
      {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(userId == null) return Task.CompletedTask;

        var user =  _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId).Result;

        if(user == null) return Task.CompletedTask;

        if(user.Role == Roles.Admin.GetStringValue()) context.Succeed(requirement);

        return Task.CompletedTask;
      }
    }
}