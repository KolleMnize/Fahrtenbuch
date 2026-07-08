using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Infrastructure.mapper;
using Fahrtenbuch.Infrastructure.records;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.persistence.repositories;

public class CarRepository(IServiceProvider serviceProvider)
{
    public void Create(Car carEntity)
    {
        var carRecord = CarRecordMapper.CarToCarRecord(carEntity);
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<CarRecord>().Add(carRecord);
        dbContext.SaveChanges();
    }

    public IEnumerable<Car> GetAll()
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<CarRecord>().Select(CarRecordMapper.CarRecordToCar).ToList();
    }
}
