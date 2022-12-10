using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System.Security.Claims;

namespace Services.Authorisation
{
  // Helper service to get back the username from the token claims
  public class UsernameAccessor : IUserNameAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UsernameAccessor(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserName()
    {
      return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
  }
}