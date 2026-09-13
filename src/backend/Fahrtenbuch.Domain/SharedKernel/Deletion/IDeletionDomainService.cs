using ErrorOr;

namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

public interface IDeletionDomainService<TId>
{
    Task<ErrorOr<Success>> EnsureDeletable(TId aggregateId, CancellationToken ct = default);
}