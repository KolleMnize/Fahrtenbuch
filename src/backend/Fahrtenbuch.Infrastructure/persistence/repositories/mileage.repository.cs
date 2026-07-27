using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.interfaces.repositories;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.mapper;
using Fahrtenbuch.Infrastructure.records;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.persistence.repositories;

public class MileageRepository(IServiceProvider serviceProvider) : IMileageRepository
{
    public void Create(Mileage mileage)
    {
        var mileageRecord = MileageRecordMapper.MileageToMileageRecord(mileage);
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<MileageRecord>().Add(mileageRecord);
        dbContext.SaveChanges();
    }

    public IEnumerable<Mileage> GetAll()
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<MileageRecord>().Select(MileageRecordMapper.MileageRecordToMileage).ToList();
    }

    public Mileage? GetFollowingMileageFromDate(CarId carId, DateTime date)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileageRecord = dbContext.Set<MileageRecord>()
            .Where(m => m.CarId == carId.Value && m.Date > date)
            .OrderBy(m => m.Date)
            .FirstOrDefault();

        return mileageRecord != null ? MileageRecordMapper.MileageRecordToMileage(mileageRecord) : null;
    }

    public Mileage? GetPreviousMileageFromDate(CarId carId, DateTime date)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileageRecord = dbContext.Set<MileageRecord>()
            .Where(m => m.CarId == carId.Value && m.Date < date)
            .OrderByDescending(m => m.Date)
            .FirstOrDefault();

        return mileageRecord != null ? MileageRecordMapper.MileageRecordToMileage(mileageRecord) : null;
    }
}