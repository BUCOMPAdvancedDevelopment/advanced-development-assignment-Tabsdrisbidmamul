using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Services.Authentication;
using Services.Authorisation;
using Services.Interfaces;

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

          var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

          string jwtKey;

          if(env == "Development") {
            jwtKey = config.GetValue<string>("JwtKey");
          } else {
            jwtKey = Environment.GetEnvironmentVariable("JwtKey");
          }

          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

          services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
              opt.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
              };
            });

          services.AddAuthorization(opt => 
          {
            opt.AddPolicy("IsAdmin", policy =>
            {
              policy.Requirements.Add(new IsAdminRequirement());
            });
          });
          services.AddTransient<IAuthorizationHandler, IsAdminRequirementHandler>();

          services.AddScoped<ITokenService, TokenService>();

          return services;

        }
    }
}