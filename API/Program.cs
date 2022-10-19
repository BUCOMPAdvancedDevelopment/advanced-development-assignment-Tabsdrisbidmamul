using API.Extensions;
using Application.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try 
{
  var context = services.GetRequiredService<DataContext>();
  
  await context.Database.MigrateAsync();
  await Seed.SeedData(context);
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

