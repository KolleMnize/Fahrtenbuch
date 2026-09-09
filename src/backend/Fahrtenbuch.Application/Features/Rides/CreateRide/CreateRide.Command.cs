using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Rides.CreateRide;

public record CreateRideCommand(string Description, Guid StartMileageId) : ICommand<RideDto>;
