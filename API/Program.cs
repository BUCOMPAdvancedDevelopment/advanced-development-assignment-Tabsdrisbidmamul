using Application.Games;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt => 
{
  opt
    .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

if(builder.Environment.IsDevelopment()) 
{
  builder.Services.AddCors(opt => 
  {
    opt.AddPolicy("CorsPolicy", policy =>
    {
      policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
  });
}


builder.Services.AddMediatR(typeof(List.Handler).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

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

