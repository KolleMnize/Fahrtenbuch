using Fahrtenbuch.Application.Dtos;

namespace Fahrtenbuch.Application.Querys;

public record GetHappeningsQueryResult(IEnumerable<HappeningDto> Result);
