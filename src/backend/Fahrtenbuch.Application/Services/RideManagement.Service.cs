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
    public async Task<ErrorOr<Success>> Handle(CreateRideCommand command)
    {
        var mileageExists = mileageRepository.Exists(MileageId.Create(command.StartMileageId).Value);
        if (!mileageExists)
            return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.StartMileageId} does not exist.");

        var ride = Ride.Create(
            RideId.Create(Guid.NewGuid()).Value,
            command.Description,
            MileageId.Create(command.StartMileageId).Value);

        rideRepository.Create(ride);

        return Result.Success;
    }

    public async Task<ErrorOr<Success>> Handle(EndRideCommand command)
    {
        var mileageExists = mileageRepository.Exists(MileageId.Create(command.EndMileageId).Value);
        if (!mileageExists)
            return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.EndMileageId} does not exist.");

        var ride = rideRepository.GetById(RideId.Create(command.RideId).Value);
        if (ride == null)
            return Error.Validation(code: "RideNotFound", description: $"Ride with id {command.RideId} does not exist.");

        Ride updatedRide = rideService.EndRide(ride, MileageId.Create(command.EndMileageId).Value);

        rideRepository.Update(updatedRide);

        return Result.Success;
    }

    public async Task<GetRidesQueryResult> Handle(GetRidesQuery query)
    {
        IEnumerable<Ride> repoResult = rideRepository.GetAll();
        IEnumerable<RideDto> rideDtos = repoResult.Select(RideDtoMapper.RideToRideDto);

        return new GetRidesQueryResult(rideDtos);
    }
}