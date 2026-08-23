using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class RideRepository(IServiceProvider serviceProvider) : IRideRepository
{
    public void Create(Ride ride)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Ride>().Add(ride);
        dbContext.SaveChanges();
    }

    public bool Exists(RideId rideId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Ride>().Any(r => r.Id.Value == rideId.Value);
    }

    public IEnumerable<Ride> GetAll()
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Ride>().ToList();
    }

    public Ride? GetById(RideId rideId)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return dbContext.Set<Ride>().FirstOrDefault(r => r.Id.Value == rideId.Value);
    }

    public void Update(Ride ride)
    {
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Ride>().Update(ride);
        dbContext.SaveChanges();
    }
}
