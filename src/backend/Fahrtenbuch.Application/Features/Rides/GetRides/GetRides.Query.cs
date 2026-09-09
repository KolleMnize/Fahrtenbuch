using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Rides.GetRides;

public record GetRidesQuery() : IQuery<GetRidesQueryResult>;