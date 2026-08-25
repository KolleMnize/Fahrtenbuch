using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;

namespace Fahrtenbuch.Infrastructure.Services;

internal class DbSeederService(
    FahrtenbuchDbContext dbContext,
    MileageDomainService mileageDomainService,
    RideDomainService rideDomainService)
{
    internal void Seed()
    {

        CarId Car1Id = CarId.Create(Guid.Parse("12473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        CarId Car2Id = CarId.Create(Guid.Parse("22473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        MileageId Mileage1Id = MileageId.Create(Guid.Parse("32473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        MileageId Mileage2Id = MileageId.Create(Guid.Parse("42473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        HappeningId Happening1Id = HappeningId.Create(Guid.Parse("52473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        HappeningId Happening2Id = HappeningId.Create(Guid.Parse("62473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        RideId Ride1Id = RideId.Create(Guid.Parse("72473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;
        RideId Ride2Id = RideId.Create(Guid.Parse("82473fc2-9ee5-4670-834c-7c8401ec2df1")).Value;

        if (!dbContext.Cars.Any())
        {
            dbContext.Cars.AddRange(
                Car.Create(Car1Id, "Car 1").Value,
                Car.Create(Car2Id, "Car 2").Value
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

        Happening happening1 = Happening.Create(
            Happening1Id,
            "Beschreibung 1",
            mileage1.Id);

        Happening happening2 = Happening.Create(
            Happening2Id,
            "Beschreibung 2");

        if (!dbContext.Happenings.Any())
        {
            dbContext.Happenings.AddRange(
                happening1,
                happening2
            );
            dbContext.SaveChanges();
        }

        Ride ride1 = Ride.Create(
            Ride1Id,
            "Beschreibung 1",
            mileage1.Id);

        Ride ride2 = Ride.Create(
            Ride2Id,
            "Beschreibung 2",
            mileage1.Id);

        if (!dbContext.Rides.Any())
        {
            dbContext.Rides.AddRange(
                ride1,
                ride2
            );
            dbContext.SaveChanges();
        }

        Ride endedRide = rideDomainService.EndRide(
            ride1,
            mileage2.Id);


        dbContext.SaveChanges();
    }
}