using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class CarRepository(FahrtenbuchDbContext dbContext) : ICarRepository
{
    public async Task Create(Car carEntity)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Car>().Add(carEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Car>> GetAll()
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Car>().ToListAsync();
    }

    public async Task<bool> Exists(CarId carId)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
        return await Task.FromResult(dbContext.Set<Car>().Any(c => c.Id.Value == carId.Value));
    }
}
