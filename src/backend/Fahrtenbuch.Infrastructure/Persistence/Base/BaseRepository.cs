using Microsoft.Extensions.Logging;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Fahrtenbuch.Infrastructure.Persistence.Base;

internal abstract class BaseRepository<TEntity, TId>(ILogger logger, FahrtenbuchDbContext dbContext)
    where TEntity : class
{
    internal FahrtenbuchDbContext DbContext { get; } = dbContext;
    public async Task<ErrorOr<Deleted>> Delete(TId id, CancellationToken ct = default)
    {
        try
        {
            var aggregate = await DbContext.Set<TEntity>().FindAsync([id], ct);
            if (aggregate == null)
            {
                return Error.NotFound("Repository.AggregateNotFound", "The specified Aggregate was not found.");
            }
            DbContext.Set<TEntity>().Remove(aggregate);
            await DbContext.SaveChangesAsync(ct);
            return Result.Deleted;
        }
        catch (OperationCanceledException)
        {
            logger.LogDebug("Operation was canceled while attempting to delete an Aggregate.");
            throw;
        }
        catch (DbUpdateException ex)
        {
            logger.LogWarning(ex, "Delete blocked by a foreign key constraint (likely a race condition).");
            return Error.Conflict("Repository.DeleteConflict",
                "The entity could not be deleted because it is still referenced by another Aggregate.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while attempting to delete an Aggregate.");
            return Error.Unexpected("Repository.UnexpectedError", "An error occurred while attempting to delete an Aggregate.");
        }

    }
}