using Domain;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Persistence
{
  /// <summary>
  /// Table information stored within this class, add any tables that are required within code above OnModelCreating
  /// </summary>
  public class DataContext : IdentityDbContext<User>
  {
    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }

    public DbSet<ProfileImage> ProfileImages { get; set; }

    public DbSet<CoverArt> CoverArt { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      // Set Postgres search indexes
      builder.Entity<Game>()
        .HasGeneratedTsVectorColumn(
          p => p.SearchVector,
          "english",
          p => new { p.Title }
        )
        .HasIndex(p => p.SearchVector)
        .HasMethod("GIN");

      // Convert categories from ICollection to List<string>
      builder.Entity<Game>()
        .Property(e => e.Category)
        .HasConversion(
          v => JsonConvert.SerializeObject(v),
          v => JsonConvert.DeserializeObject<List<string>>(v)
        );

      // cascade deletion for Cover Art records
      builder.Entity<Game>()
        .HasMany(c => c.CoverArt)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
    }

  }
}