namespace Fahrtenbuch.Application.Features.Happenings.GetHappenings;

public record GetHappeningsQueryResult(IEnumerable<HappeningDto> Result);
