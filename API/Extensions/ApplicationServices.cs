using Application.Games;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

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
          return services;
        }
    }
}