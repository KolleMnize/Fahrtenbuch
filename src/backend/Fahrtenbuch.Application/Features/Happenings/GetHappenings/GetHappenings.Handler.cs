using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;

namespace Fahrtenbuch.Application.Features.Happenings.GetHappenings;

public class GetHappeningsHandler(IHappeningRepository happeningRepository) : IQueryHandler<GetHappeningsQuery, GetHappeningsQueryResult>
{
    public async Task<ErrorOr<GetHappeningsQueryResult>> Handle(GetHappeningsQuery query, CancellationToken ct = default)
    {
        ErrorOr<IReadOnlyList<Happening>> repoResult = await happeningRepository.GetAll(ct);
        if (repoResult.IsError)
            return repoResult.Errors;

        IReadOnlyList<HappeningDto> happeningDtos = repoResult.Value.Select(HappeningDtoMapper.HappeningToHappeningDto).ToList();

        return new GetHappeningsQueryResult(happeningDtos);
    }
}