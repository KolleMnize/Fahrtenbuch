namespace Fahrtenbuch.Infrastructure.records;

internal record MileageRecord(
    Guid Id,
    Guid CarId,
    decimal Value,
    DateTime Date
);