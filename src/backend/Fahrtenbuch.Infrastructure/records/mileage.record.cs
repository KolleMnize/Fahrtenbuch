namespace Fahrtenbuch.Infrastructure.records;

public record MileageRecord(
    Guid Id,
    Guid CarId,
    decimal Value,
    DateTime Date
);