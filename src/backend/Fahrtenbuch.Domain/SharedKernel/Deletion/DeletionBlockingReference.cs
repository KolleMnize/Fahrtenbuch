namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

public record DeletionBlockingReference(IAggregate BlockedAggregate, IAggregate BlockingAggregate);