namespace Fahrtenbuch.Domain.SharedKernel.Deletion;

public record BlockingReason(Type AggregateType, Guid AggregateId, string Message);