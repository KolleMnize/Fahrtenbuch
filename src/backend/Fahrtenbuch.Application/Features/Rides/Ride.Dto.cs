namespace Fahrtenbuch.Application.Features.Rides;

public record RideDto(Guid RideId, string Description, Guid StartMileageId, Guid? EndMileageId);