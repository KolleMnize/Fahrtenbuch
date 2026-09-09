namespace Fahrtenbuch.Application.Features.Rides.GetRides;

public record GetRidesQueryResult(IReadOnlyList<RideDto> Rides);
