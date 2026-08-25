using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class MileageRepository(FahrtenbuchDbContext dbContext) : IMileageRepository
{
    public async Task Create(Mileage mileage)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        dbContext.Set<Mileage>().Add(mileage);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Mileage>> GetAll()
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Mileage>().ToListAsync();
    }

    public async Task<Mileage?> GetFollowingMileageFromDate(CarId carId, DateTime date)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileage = dbContext.Set<Mileage>()
            .Where(m => m.CarId.Value == carId.Value && m.Date > date)
            .OrderBy(m => m.Date)
            .FirstOrDefaultAsync();

        return await mileage != null ? await mileage : null;
    }

    public async Task<Mileage?> GetPreviousMileageFromDate(CarId carId, DateTime date)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        var mileage = dbContext.Set<Mileage>()
            .Where(m => m.CarId.Value == carId.Value && m.Date < date)
            .OrderByDescending(m => m.Date)
            .FirstOrDefaultAsync();

        return await mileage != null ? await mileage : null;
    }

    public async Task<bool> Exists(MileageId mileageId)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Mileage>().AnyAsync(m => m.Id.Value == mileageId.Value);
    }

    public async Task<Mileage?> GetById(MileageId mileageId)
    {
        // var scope = serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

        return await dbContext.Set<Mileage>().FirstOrDefaultAsync(m => m.Id.Value == mileageId.Value);
    }
}