using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class MileageRepository(IServiceProvider serviceProvider) : IMileageRepository
{
    public void Create(Mileage mileage)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Mileage>().Add(mileage);
        dbContext.SaveChanges();
    }

    public IEnumerable<Mileage> GetAll()
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Mileage>().ToList();
    }

    public Mileage? GetFollowingMileageFromDate(CarId carId, DateTime date)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileage = dbContext.Set<Mileage>()
            .Where(m => m.CarId.Value == carId.Value && m.Date > date)
            .OrderBy(m => m.Date)
            .FirstOrDefault();

        return mileage != null ? mileage : null;
    }

    public Mileage? GetPreviousMileageFromDate(CarId carId, DateTime date)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileage = dbContext.Set<Mileage>()
            .Where(m => m.CarId.Value == carId.Value && m.Date < date)
            .OrderByDescending(m => m.Date)
            .FirstOrDefault();

        return mileage != null ? mileage : null;
    }

    public bool Exists(MileageId mileageId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Mileage>().Any(m => m.Id.Value == mileageId.Value);
    }

    public Mileage? GetById(MileageId mileageId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Mileage>().FirstOrDefault(m => m.Id.Value == mileageId.Value);
    }
}