using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class MileageRepository(FahrtenbuchDbContext dbContext, ILogger<MileageRepository> logger) : IMileageRepository
{
    public async Task<ErrorOr<Success>> Create(Mileage mileage, CancellationToken ct = default)
    {
        try
        {
            dbContext.Mileages.Add(mileage);
            await dbContext.SaveChangesAsync(ct);
            return Result.Success;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while creating mileage.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating mileage.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while creating mileage.");
        }
    }

    public async Task<ErrorOr<IReadOnlyList<Mileage>>> GetAll(CancellationToken ct = default)
    {

        try
        {
            var dbResult = await dbContext.Mileages.ToListAsync(ct);
            return dbResult;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while retrieving all mileages.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all mileages.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while retrieving all mileages.");
        }
    }

    public async Task<ErrorOr<Mileage?>> GetFollowingMileageFromDate(CarId carId, DateTime date, CancellationToken ct = default)
    {
        try
        {
            var mileage = await dbContext.Mileages
                .Where(m => m.CarId.Value == carId.Value && m.Date > date)
                .OrderBy(m => m.Date)
                .FirstOrDefaultAsync(ct);

            return mileage;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while retrieving following mileage.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving following mileage.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while retrieving following mileage.");
        }
    }

    public async Task<ErrorOr<Mileage?>> GetPreviousMileageFromDate(CarId carId, DateTime date, CancellationToken ct = default)
    {
        try
        {
            var mileage = await dbContext.Mileages
                .Where(m => m.CarId.Value == carId.Value && m.Date < date)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync(ct);

            return mileage;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while retrieving previous mileage.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving previous mileage.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while retrieving previous mileage.");
        }
    }

    public async Task<ErrorOr<bool>> Exists(MileageId mileageId, CancellationToken ct = default)
    {
        try
        {
            return await dbContext.Mileages.AnyAsync(m => m.Id.Value == mileageId.Value, ct);
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while checking if mileage exists.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while checking if mileage exists.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while checking if mileage exists.");
        }
    }

    public async Task<ErrorOr<Mileage?>> GetById(MileageId mileageId, CancellationToken ct = default)
    {
        try
        {
            return await dbContext.Mileages.FirstOrDefaultAsync(m => m.Id.Value == mileageId.Value, ct);
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Operation was canceled while retrieving mileage by ID.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving mileage by ID.");
            return Error.Unexpected("Mileage.UnexpectedError", "An unexpected error occurred while retrieving mileage by ID.");
        }
    }
}