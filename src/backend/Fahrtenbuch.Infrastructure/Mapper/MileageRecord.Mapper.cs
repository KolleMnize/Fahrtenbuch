using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Records;

namespace Fahrtenbuch.Infrastructure.Mapper;

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