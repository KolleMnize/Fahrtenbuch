using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.records;

namespace Fahrtenbuch.Infrastructure.mapper;

internal static class CarRecordMapper
{
    public static CarRecord CarToCarRecord(Car car)
        => new CarRecord(car.Id.Value, car.Name);

    public static Car CarRecordToCar(CarRecord carRecord)
        => Car.Rehydrate(CarId.Create(carRecord.Id).Value, carRecord.Name);
}