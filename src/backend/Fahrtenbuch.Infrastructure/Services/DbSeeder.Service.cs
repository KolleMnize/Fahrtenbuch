using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;

namespace Fahrtenbuch.Infrastructure.Services;

internal class DbSeederService(FahrtenbuchDbContext dbContext, MileageDomainService mileageDomainService)
{
    internal void Seed()
    {

        CarId Car1Id = CarId.Create(Guid.Parse("12473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        CarId Car2Id = CarId.Create(Guid.Parse("22473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        MileageId Mileage1Id = MileageId.Create(Guid.Parse("32473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        MileageId Mileage2Id = MileageId.Create(Guid.Parse("42473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        if (!dbContext.Cars.Any())
        {
            dbContext.Cars.AddRange(
                Car.Create(Car1Id, "Car 1"),
                Car.Create(Car2Id, "Car 2")
            );
            dbContext.SaveChanges();
        }

        Mileage mileage1 = mileageDomainService.CreateMileage(
                    Mileage1Id,
                    Car1Id,
                    100,
                    DateTime.Parse("2024-01-01T00:00:00Z"));
        Mileage mileage2 = mileageDomainService.CreateMileage(
                    Mileage2Id,
                    Car2Id,
                    200,
                    DateTime.Parse("2024-01-03T00:00:00Z"));


        if (!dbContext.Mileages.Any())
        {
            dbContext.Mileages.AddRange(
                mileage1,
                mileage2
            );
            dbContext.SaveChanges();
        }
    }
}
