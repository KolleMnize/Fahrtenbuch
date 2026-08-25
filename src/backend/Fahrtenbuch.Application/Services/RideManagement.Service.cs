using ErrorOr;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Application.Mapper;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.Services;
using Fahrtenbuch.Domain.ValueObjects;

namespace Fahrtenbuch.Application.Services;

public class RideManagementService(
    IMileageRepository mileageRepository,
    IRideRepository rideRepository,
    RideDomainService rideService
    )
{
    public async Task<ErrorOr<Ride>> Handle(CreateRideCommand command)
    {
        var mileageExists = mileageRepository.Exists(MileageId.Create(command.StartMileageId).Value);
        if (!mileageExists)
            return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.StartMileageId} does not exist.");

        var rideCreateResult = Ride.Create(
            RideId.Create(Guid.NewGuid()).Value,
            command.Description,
            MileageId.Create(command.StartMileageId).Value);

        if (rideCreateResult.IsError)
        {
            return rideCreateResult.Errors;
        }

        rideRepository.Create(rideCreateResult.Value);

        return rideCreateResult.Value;
    }

    public async Task<ErrorOr<Ride>> Handle(EndRideCommand command)
    {
        var mileageExists = mileageRepository.Exists(MileageId.Create(command.EndMileageId).Value);
        if (!mileageExists)
            return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.EndMileageId} does not exist.");

        var ride = rideRepository.GetById(RideId.Create(command.RideId).Value);
        if (ride == null)
            return Error.Validation(code: "RideNotFound", description: $"Ride with id {command.RideId} does not exist.");

        var rideEndRideResult = rideService.EndRide(ride, MileageId.Create(command.EndMileageId).Value);

        if (rideEndRideResult.IsError)
        {
            return rideEndRideResult.Errors;
        }

        rideRepository.Update(rideEndRideResult.Value);

        return rideEndRideResult.Value;
    }

    public async Task<GetRidesQueryResult> Handle(GetRidesQuery query)
    {
        IEnumerable<Ride> repoResult = rideRepository.GetAll();
        IEnumerable<RideDto> rideDtos = repoResult.Select(RideDtoMapper.RideToRideDto);

        return new GetRidesQueryResult(rideDtos);
    }
}