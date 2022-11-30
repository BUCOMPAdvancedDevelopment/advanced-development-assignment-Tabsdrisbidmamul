using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace API.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config) 
        {
          services.AddIdentityCore<User>(opt =>
          {
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredLength = 10;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireDigit = true;

          })
          .AddEntityFrameworkStores<DataContext>()
          .AddSignInManager<SignInManager<User>>();

          services.AddAuthentication();

          return services;

        }
    }
}