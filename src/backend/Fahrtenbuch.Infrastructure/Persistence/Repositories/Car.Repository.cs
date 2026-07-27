using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Mapper;
using Fahrtenbuch.Infrastructure.Records;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class CarRepository(IServiceProvider serviceProvider) : ICarRepository
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

    public bool Exists(CarId carId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
        return dbContext.Set<CarRecord>().Any(c => c.Id == carId.Value);
    }
}
