using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class CarRepository(FahrtenbuchDbContext dbContext, ILogger<CarRepository> logger)
: BaseRepository<Car, CarId>(logger, dbContext), ICarRepository
{
    public async Task<ErrorOr<bool>> Exists(CarId carId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbresult = await DbContext.Cars.AnyAsync(c => c.Id.Value == carId.Value, cancellationToken);
            return dbresult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("The operation was canceled while checking if a car exists.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while checking if a car exists.");
            return Error.Unexpected("Car.UnexpectedError", "An error occurred while checking if a car exists.");
        }
    }

    public async Task<ErrorOr<Success>> Create(Car carEntity, CancellationToken cancellationToken = default)
    {
        try
        {
            DbContext.Cars.Add(carEntity);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("The operation was canceled while creating a car.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a car.");
            return Error.Unexpected("Car.UnexpectedError", "An error occurred while creating a car.");
        }
    }

    public async Task<ErrorOr<IReadOnlyList<Car>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var dbresult = await DbContext.Cars.ToListAsync(cancellationToken);
            return dbresult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("The operation was canceled while retrieving all cars.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all cars.");
            return Error.Unexpected("Car.UnexpectedError", "An error occurred while retrieving all cars.");
        }
    }


}
