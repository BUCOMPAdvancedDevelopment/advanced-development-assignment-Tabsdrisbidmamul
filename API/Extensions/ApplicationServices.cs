using Application.Games;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Services.CloudinaryAccessor;

namespace API.Extensions
{
  public static class ApplicationServices
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      // Make every business endpoint requires authorisation
      services.AddControllers(opt => 
      {
        var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();

        opt.Filters.Add(new AuthorizeFilter(policy));
      })
      .AddFluentValidation(config => 
      {
        config.RegisterValidatorsFromAssemblyContaining<Create>();
      });

      services.AddPostgress(config);


      services.AddCors(opt => 
      {
        opt.AddPolicy("AllowAll", policy =>
        {
          policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
        });
      });


      services.AddMediatR(typeof(List.Handler).Assembly);
      
      services.AddAutoMapper(typeof(Application.Core.MappingProfiles).Assembly);


      services.AddScoped<ICloudinaryPhoto, CloudinaryPhotoService>();

      // GCP for the name value use CloudinarySettings:CloudName etc.
      services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

      return services;
    }
  }
}