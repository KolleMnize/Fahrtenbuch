using ErrorOr;
using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Interfaces.Repositories;

namespace Fahrtenbuch.Application.Features.Mileages.GetMileages;

public class GetMileagesHandler(IMileageRepository mileageRepository) : IQueryHandler<GetMileagesQuery, GetMileagesQueryResult>
{
    public async Task<ErrorOr<GetMileagesQueryResult>> Handle(GetMileagesQuery query, CancellationToken ct = default)
    {
        var getAllResult = await mileageRepository.GetAll(ct);
        if (getAllResult.IsError)
            return getAllResult.Errors;

        IReadOnlyList<MileageDto> mileageDtos = getAllResult.Value.Select(MileageDtoMapper.MileageToMileageDto).ToList();
        return new GetMileagesQueryResult(mileageDtos);
    }
}