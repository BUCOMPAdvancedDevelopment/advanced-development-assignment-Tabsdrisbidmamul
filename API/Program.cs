using Application.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => 
{
  if(builder.Environment.IsDevelopment()) {
      opt
    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
  } else {
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_NAME");

    Console.WriteLine($"connectionString {connectionString}");

    opt
      .UseSqlite($"Data source={connectionString}");
  }
});


builder.Services.AddCors(opt => 
{
  opt.AddPolicy("AllowAll", policy =>
  {
    policy
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowAnyOrigin();
  });
});


builder.Services.AddMediatR(typeof(List.Handler).Assembly);


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

