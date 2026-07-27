using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.ValueObjects;
using Fahrtenbuch.Infrastructure.Records;

namespace Fahrtenbuch.Infrastructure.Mapper;

internal static class CarRecordMapper
{
    public static CarRecord CarToCarRecord(Car car)
        => new CarRecord(car.Id.Value, car.Name);

    public static Car CarRecordToCar(CarRecord carRecord)
        => Car.Rehydrate(CarId.Create(carRecord.Id).Value, carRecord.Name);
}