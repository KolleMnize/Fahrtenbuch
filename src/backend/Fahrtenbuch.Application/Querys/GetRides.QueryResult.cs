using Fahrtenbuch.Application.Dtos;

namespace Fahrtenbuch.Application.Querys;

public record GetRidesQueryResult(IEnumerable<RideDto> Rides);
