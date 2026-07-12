using Fahrtenbuch.Application.dtos;

namespace Fahrtenbuch.Application.querys;

public record GetMileagesQueryResult(IEnumerable<MileageDto> Mileages);
