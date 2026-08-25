using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class RideRepository(FahrtenbuchDbContext dbContext) : IRideRepository
{
    public async Task Create(Ride ride)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Ride>().Add(ride);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(RideId rideId)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Ride>().AnyAsync(r => r.Id.Value == rideId.Value);
    }

    public async Task<IEnumerable<Ride>> GetAll()
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Ride>().ToListAsync();
    }

    public async Task<Ride?> GetById(RideId rideId)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Ride>().FirstOrDefaultAsync(r => r.Id.Value == rideId.Value);
    }

    public async Task Update(Ride ride)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
        var entry = dbContext.Entry(ride);
        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
        {
            dbContext.Set<Ride>().Attach(ride);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        await dbContext.SaveChangesAsync();
    }
}
