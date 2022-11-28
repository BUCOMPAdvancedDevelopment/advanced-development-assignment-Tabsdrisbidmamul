using Application.Games;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Services.CloudinaryAccessor;

namespace API.Extensions
{
  public static class ApplicationServices
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddControllers().AddFluentValidation(config => 
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

      services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

      return services;
    }
  }
}