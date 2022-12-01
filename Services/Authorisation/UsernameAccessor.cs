using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System.Security.Claims;

namespace Services.Authorisation
{
  public class UsernameAccessor : IUsernameAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UsernameAccessor(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetUsername()
    {
      return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
  }
}