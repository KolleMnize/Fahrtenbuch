namespace Fahrtenbuch.Application.dtos;

public record MileageDto(Guid MileageId, Guid CarId, decimal Value, DateTime Date);
