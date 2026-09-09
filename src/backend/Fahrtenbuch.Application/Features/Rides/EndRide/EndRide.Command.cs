using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Rides.EndRide;

public record EndRideCommand(Guid RideId, Guid EndMileageId) : ICommand<RideDto>;