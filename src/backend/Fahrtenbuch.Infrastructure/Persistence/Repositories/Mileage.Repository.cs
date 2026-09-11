using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

internal class MileageRepository(FahrtenbuchDbContext dbContext, ILogger<MileageRepository> logger)
: BaseRepository<Mileage, MileageId>(logger, dbContext), IMileageRepository
{
    public async Task<ErrorOr<Success>> Create(Mileage mileage, CancellationToken ct = default)
    {
        try
        {
            DbContext.Mileages.Add(mileage);
            await DbContext.SaveChangesAsync(ct);
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
            var dbResult = await DbContext.Mileages.ToListAsync(ct);
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
            var mileage = await DbContext.Mileages
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
            var mileage = await DbContext.Mileages
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
            return await DbContext.Mileages.AnyAsync(m => m.Id.Value == mileageId.Value, ct);
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
            return await DbContext.Mileages.FirstOrDefaultAsync(m => m.Id.Value == mileageId.Value, ct);
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