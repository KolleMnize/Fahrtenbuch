namespace Fahrtenbuch.Application.Dtos;

public record RideDto(Guid RideId, string Description, Guid StartMileageId, Guid? EndMileageId);