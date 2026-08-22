using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Application.Mapper;

internal static class HappeningDtoMapper
{
    public static HappeningDto HappeningToHappeningDto(Happening happening)
    {
        return new HappeningDto(
            happening.Id.Value,
            happening.Description,
            happening.MileageId != null ? happening.MileageId.Value : null);
    }
}
