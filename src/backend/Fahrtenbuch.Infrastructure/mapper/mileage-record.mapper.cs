using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.records;

namespace Fahrtenbuch.Infrastructure.mapper;

internal static class MileageRecordMapper
{
    internal static Mileage MileageRecordToMileage(MileageRecord record)
    => Mileage.Rehydrate(
            MileageId.Create(record.Id).Value,
            CarId.Create(record.CarId).Value,
            record.Value,
            record.Date);

    internal static MileageRecord MileageToMileageRecord(Mileage mileage)
    => new MileageRecord(
            mileage.Id.Value,
            mileage.CarId.Value,
            mileage.Value,
            mileage.Date);
}