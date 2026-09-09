using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories
{
    public class HappeningRepository(FahrtenbuchDbContext dbContext, ILogger<HappeningRepository> logger) : IHappeningRepository
    {
        public async Task<ErrorOr<Success>> Create(Happening happening, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbResult = dbContext.Happenings.Add(happening);
                await dbContext.SaveChangesAsync(cancellationToken);
                return Result.Success;
            }
            catch (OperationCanceledException)
            {
                logger.LogDebug("Operation canceled while creating a happening");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a happening");
                return Error.Unexpected("Happening.UnexpectedError", "An error occurred while creating a happening");
            }
        }

        public async Task<ErrorOr<IReadOnlyList<Happening>>> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var dbResult = await dbContext.Happenings.ToListAsync(cancellationToken);
                return dbResult;
            }
            catch (OperationCanceledException)
            {
                logger.LogDebug("Operation was canceled while retrieving all happenings");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving all happenings");
                return Error.Unexpected("Happening.UnexpectedError", "An error occurred while retrieving all happenings");
            }
        }
    }
}