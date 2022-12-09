using System.IO.Compression;
using API.Extensions;
using API.Middleware;
using Application.Games;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Persistence;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddResponseCompression(options =>
{
  options.Providers.Add<GzipCompressionProvider>();
  options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { 
  "image/jpeg", "image/png", "application/javascript", "application/font-woff2", "image/svg+xml"});
  options.EnableForHttps = true;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
  options.Level = CompressionLevel.Optimal;
});

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseResponseCompression();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try 
{
  var context = services.GetRequiredService<DataContext>();
  var userManager = services.GetRequiredService<UserManager<User>>();

  await context.Database.MigrateAsync();
  await Seed.SeedData(context, userManager);
}
catch (Exception e) 
{
  var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(e, "An error occured during migration!");
}

if(app.Environment.IsProduction()) 
{
  var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
  var url = $"http://0.0.0.0:{port}";
  var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

  await app.RunAsync(url);
} 
else
{
  await app.RunAsync();
} 

