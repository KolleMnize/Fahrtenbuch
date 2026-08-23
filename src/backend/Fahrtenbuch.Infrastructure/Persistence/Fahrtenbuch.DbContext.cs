using Fahrtenbuch.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Fahrtenbuch.Infrastructure.Persistence;

public class FahrtenbuchDbContext : DbContext
{
    public FahrtenbuchDbContext(DbContextOptions<FahrtenbuchDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
        .UseInMemoryDatabase("Fahrtenbuch");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FahrtenbuchDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    internal DbSet<Car> Cars { get; set; } = null!;
    internal DbSet<Mileage> Mileages { get; set; } = null!;
    internal DbSet<Happening> Happenings { get; set; } = null!;
    internal DbSet<Ride> Rides { get; set; } = null!;

}
