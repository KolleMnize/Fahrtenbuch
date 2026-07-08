using Fahrtenbuch.Infrastructure.records;
using Microsoft.EntityFrameworkCore;

namespace Fahrtenbuch.Infrastructure.persistence;

public class FahrtenbuchDbContext : DbContext
{
    public FahrtenbuchDbContext(DbContextOptions<FahrtenbuchDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<CarRecord>().HasKey(c => c.Id);

        base.OnModelCreating(modelBuilder);
    }

    internal DbSet<CarRecord> CarRecords { get; set; } = null!;

}
