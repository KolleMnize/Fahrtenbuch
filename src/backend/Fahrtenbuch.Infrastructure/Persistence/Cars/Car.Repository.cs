using ErrorOr;
using Fahrtenbuch.Domain.Cars;
using Fahrtenbuch.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Cars;

internal class CarRepository(FahrtenbuchDbContext dbContext, ILogger<CarRepository> logger)
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

    public async Task<ErrorOr<Car>> GetById(CarId carId, CancellationToken cancellationToken)
    {
        try
        {
            var dbresult = await DbContext.Cars.FirstOrDefaultAsync(c => c.Id.Value == carId.Value, cancellationToken);
            if (dbresult == null)
                return Error.NotFound("Car.NotFound", $"The car with the specified ID {carId.Value} was not found.");
            return dbresult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("The operation was canceled while retrieving a car by ID.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving a car by ID.");
            return Error.Unexpected("Car.UnexpectedError", "An error occurred while retrieving a car by ID.");
        }
    }
}
