using Fahrtenbuch.Infrastructure.Records;
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
        modelBuilder.Entity<CarRecord>()
        .HasKey(c => c.Id);
        modelBuilder.Entity<MileageRecord>()
        .HasKey(m => m.Id);
        modelBuilder.Entity<MileageRecord>()
        .HasIndex(m => m.CarId);

        base.OnModelCreating(modelBuilder);

        UseSeeding(modelBuilder);
    }

    internal DbSet<CarRecord> CarRecords { get; set; } = null!;
    internal DbSet<MileageRecord> MileageRecords { get; set; } = null!;

    private static void UseSeeding(ModelBuilder modelBuilder)
    {
        Guid Car1Id = Guid.Parse("12473fc2-9ee5-4670-834c-7c8401ec2df1");
        Guid Car2Id = Guid.Parse("22473fc2-9ee5-4670-834c-7c8401ec2df1");

        modelBuilder.Entity<CarRecord>().HasData(
            new CarRecord(Car1Id, "Car 1"),
            new CarRecord(Car2Id, "Car 2")
        );

        modelBuilder.Entity<MileageRecord>().HasData(
            new MileageRecord(
                Guid.Parse("32473fc2-9ee5-4670-834c-7c8401ec2df1"),
                Car1Id,
                100,
                DateTime.Parse("2024-01-01T00:00:00Z")),
            new MileageRecord(
                Guid.Parse("42473fc2-9ee5-4670-834c-7c8401ec2df1"),
                Car1Id,
                200,
                DateTime.Parse("2024-01-03T00:00:00Z"))
        );
    }

}
