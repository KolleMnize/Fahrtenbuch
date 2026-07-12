using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Domain.aggregates;

namespace Fahrtenbuch.Application.mapper;

internal static class MileageDtoMapper
{
    public static MileageDto MileageToMileageDto(Mileage mileage)
    {
        return new MileageDto(
            mileage.Id.Value,
            mileage.CarId.Value,
            mileage.Value,
            mileage.Date
        );
    }
}
