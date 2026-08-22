using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
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
        UseSeeding(modelBuilder);
    }

    internal DbSet<Car> Cars { get; set; } = null!;
    internal DbSet<Mileage> Mileages { get; set; } = null!;
    internal DbSet<Happening> Happenings { get; set; } = null!;

    private void UseSeeding(ModelBuilder modelBuilder)
    {

        CarId Car1Id = CarId.Create(Guid.Parse("12473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        CarId Car2Id = CarId.Create(Guid.Parse("22473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        MileageId Mileage1Id = MileageId.Create(Guid.Parse("32473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        MileageId Mileage2Id = MileageId.Create(Guid.Parse("42473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        modelBuilder.Entity<Car>().HasData(
            Car.Create(Car1Id, "Car 1"),
            Car.Create(Car2Id, "Car 2")
        );

        Mileage mileage1 = Mileage.Create(
                    Mileage1Id,
                    Car1Id,
                    100,
                    DateTime.Parse("2024-01-01T00:00:00Z"));
        Mileage mileage2 = Mileage.Create(
                    Mileage2Id,
                    Car2Id,
                    200,
                    DateTime.Parse("2024-01-03T00:00:00Z"));


        modelBuilder.Entity<Mileage>().HasData(
           mileage1,
           mileage2
        );

    }

}
