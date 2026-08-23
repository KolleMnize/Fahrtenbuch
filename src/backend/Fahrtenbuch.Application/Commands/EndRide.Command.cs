namespace Fahrtenbuch.Application.Commands;

public record EndRideCommand(Guid RideId, Guid EndMileageId);