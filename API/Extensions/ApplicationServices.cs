using Application.Games;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Services.Authorisation;
using Services.CloudinaryAccessor;
using Services.Interfaces;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Services.Firestore;
using API.Models;
using System.Text.Json;

namespace API.Extensions
{ 
  /// <summary>
  /// Service Extensions for the application - Controllers, CORS, DI services
  /// </summary>
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

      // Add SQL connection
      services.AddPostgress(config);


      services.AddCors(opt => 
      {
        opt.AddPolicy("AllowAll", policy =>
        {
          policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .WithExposedHeaders("WWW-Authenticate");
        });
      });

      // Business Services
      services.AddMediatR(typeof(List.Handler).Assembly);
      
      services.AddAutoMapper(typeof(Application.Core.MappingProfiles).Assembly);

      var firebaseSettings = new FirebaseSettings();
      var firebaseJson = JsonSerializer.Serialize(firebaseSettings);


      services.AddSingleton(_ => new FirestoreProvider(
        new FirestoreDbBuilder
        {
          ProjectId = firebaseSettings.ProjectId,
          JsonCredentials = firebaseJson
        }.Build()
      ));


      services.AddScoped<ICloudinaryPhoto, CloudinaryPhotoService>();
      services.AddScoped<IUserNameAccessor, UsernameAccessor>();

      // GCP for the name value use CloudinarySettings:CloudName etc.
      services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

      return services;
    }
  }
}