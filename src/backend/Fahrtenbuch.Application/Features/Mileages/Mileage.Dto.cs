namespace Fahrtenbuch.Application.Features.Mileages;

public record MileageDto(Guid MileageId, Guid CarId, decimal Value, DateTime Date);
