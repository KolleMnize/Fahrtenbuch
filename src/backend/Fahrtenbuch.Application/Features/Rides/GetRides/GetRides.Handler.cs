using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;

namespace Fahrtenbuch.Application.Features.Rides.GetRides;

public class GetRidesHandler(IRideRepository rideRepository) : IQueryHandler<GetRidesQuery, GetRidesQueryResult>
{
    public async Task<ErrorOr<GetRidesQueryResult>> Handle(GetRidesQuery query, CancellationToken ct = default)
    {
        ErrorOr<IReadOnlyList<Ride>> repoResult = await rideRepository.GetAll(ct);
        if (repoResult.IsError)
            return repoResult.Errors;

        IReadOnlyList<RideDto> rideDtos = repoResult.Value.Select(RideDtoMapper.RideToRideDto).ToList();

        return new GetRidesQueryResult(rideDtos);
    }
}