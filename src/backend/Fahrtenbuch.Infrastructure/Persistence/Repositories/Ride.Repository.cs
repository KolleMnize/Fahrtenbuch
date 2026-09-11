using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Microsoft.Extensions.Logging;
using Fahrtenbuch.Infrastructure.Persistence.Base;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories;

public class RideRepository(FahrtenbuchDbContext dbContext, ILogger<RideRepository> logger)
: BaseRepository<Ride, RideId>(logger, dbContext), IRideRepository
{
    public async Task<ErrorOr<Success>> Create(Ride ride, CancellationToken cancellationToken = default)
    {
        try
        {
            DbContext.Rides.Add(ride);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while creating a ride.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating a ride.");
            return Error.Unexpected("Ride.UnexpectedError", "An unexpected error occurred while creating a ride.");
        }
    }

    public async Task<ErrorOr<bool>> Exists(RideId rideId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbResult = await DbContext.Rides.AnyAsync(r => r.Id.Value == rideId.Value, cancellationToken);
            return dbResult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while checking if a ride exists.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while checking if a ride exists.");
            return Error.Unexpected("Ride.UnexpectedError", "An unexpected error occurred while checking if a ride exists.");
        }
    }

    public async Task<ErrorOr<IReadOnlyList<Ride>>> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            var dbResult = await DbContext.Rides.ToListAsync(cancellationToken);
            return dbResult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while retrieving all rides.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving all rides.");
            return Error.Unexpected("Ride.UnexpectedError", "An unexpected error occurred while retrieving all rides.");
        }
    }

    public async Task<ErrorOr<Ride?>> GetById(RideId rideId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dbResult = await DbContext.Rides.FirstOrDefaultAsync(r => r.Id.Value == rideId.Value, cancellationToken);
            return dbResult;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while retrieving a ride by ID.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving a ride by ID.");
            return Error.Unexpected("Ride.UnexpectedError", "An unexpected error occurred while retrieving a ride by ID.");
        }
    }

    public async Task<ErrorOr<Success>> Update(Ride ride, CancellationToken cancellationToken = default)
    {
        try
        {
            var entry = DbContext.Entry(ride);
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                DbContext.Rides.Attach(ride);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            await DbContext.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while updating a ride.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating a ride.");
            return Error.Unexpected("Ride.UnexpectedError", "An unexpected error occurred while updating a ride.");
        }
    }
}
