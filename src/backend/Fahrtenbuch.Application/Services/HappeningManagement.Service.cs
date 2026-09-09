using ErrorOr;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Dtos;
using Fahrtenbuch.Application.Mapper;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.ValueObjects;


namespace Fahrtenbuch.Application.Services
{
    public class HappeningManagementService(
        IHappeningRepository happeningRepository,
        IMileageRepository mileageRepository)
    {
        public async Task<GetHappeningsQueryResult> Handle(GetHappeningsQuery query)
        {
            IEnumerable<Happening> repoResult = await happeningRepository.GetAll();
            IEnumerable<HappeningDto> happeningDtos = repoResult.Select(HappeningDtoMapper.HappeningToHappeningDto);

            return new GetHappeningsQueryResult(happeningDtos);
        }
    }
}