using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Application.Features.Mileages;

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
