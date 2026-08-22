namespace Fahrtenbuch.Application.Dtos;

public record HappeningDto(Guid HappeningId, string Description, Guid? MileageId);