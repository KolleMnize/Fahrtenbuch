namespace Fahrtenbuch.Infrastructure.Records;

internal record MileageRecord(
    Guid Id,
    Guid CarId,
    decimal Value,
    DateTime Date
);