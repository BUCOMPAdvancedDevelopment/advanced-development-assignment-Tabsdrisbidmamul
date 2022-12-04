using Domain;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  public class DataContext : IdentityDbContext<User>
  {
    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }

    public DbSet<ProfileImage> ProfileImages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Game>()
        .HasGeneratedTsVectorColumn(
          p => p.SearchVector,
          "english",
          p => new { p.Title }
        )
        .HasIndex(p => p.SearchVector)
        .HasMethod("GIN");
    }

  }
}