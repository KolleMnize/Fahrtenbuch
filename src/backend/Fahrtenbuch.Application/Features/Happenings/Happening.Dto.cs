namespace Fahrtenbuch.Application.Features.Happenings;

public record HappeningDto(Guid HappeningId, string Description, Guid? MileageId);