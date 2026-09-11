using ErrorOr;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories
{
    public class HappeningRepository(FahrtenbuchDbContext dbContext, ILogger<HappeningRepository> logger)
    : BaseRepository<Happening, HappeningId>(logger, dbContext), IHappeningRepository
    {
        public async Task<ErrorOr<Success>> Create(Happening happening, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbResult = DbContext.Happenings.Add(happening);
                await DbContext.SaveChangesAsync(cancellationToken);
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
                var dbResult = await DbContext.Happenings.ToListAsync(cancellationToken);
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