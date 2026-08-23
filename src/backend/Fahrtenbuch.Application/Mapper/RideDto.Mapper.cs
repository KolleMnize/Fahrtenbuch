using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Application.Mapper;

public static class RideDtoMapper
{
    public static RideDto RideToRideDto(Ride ride)
    {
        return new RideDto(
            ride.Id.Value,
            ride.Description,
            ride.StartMileageId.Value,
            ride.EndMileageId?.Value);
    }
}
