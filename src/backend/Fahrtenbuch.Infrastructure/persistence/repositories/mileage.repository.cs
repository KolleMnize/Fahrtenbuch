using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Infrastructure.mapper;
using Fahrtenbuch.Infrastructure.records;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.persistence.repositories;

public class MileageRepository(IServiceProvider serviceProvider)
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
}