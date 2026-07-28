using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class CarRepository(IServiceProvider serviceProvider) : ICarRepository
{
    public void Create(Car carEntity)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Car>().Add(carEntity);
        dbContext.SaveChanges();
    }

    public IEnumerable<Car> GetAll()
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Car>().ToList();
    }

    public bool Exists(CarId carId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
        return dbContext.Set<Car>().Any(c => c.Id.Value == carId.Value);
    }
}
