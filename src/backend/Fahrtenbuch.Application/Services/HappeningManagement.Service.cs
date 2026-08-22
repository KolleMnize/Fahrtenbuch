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
        public async Task<ErrorOr<Success>> Handle(CreateHappeningCommand command)
        {
            var mileageExists = mileageRepository.Exists(MileageId.Create(command.MileageId).Value);
            if (!mileageExists)
                return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.MileageId} does not exist.");

            var happening = Happening.Create(
                HappeningId.Create(Guid.NewGuid()).Value,
                command.Description,
                MileageId.Create(command.MileageId).Value);

            happeningRepository.Create(happening);
            return Result.Success;
        }

        public async Task<GetHappeningsQueryResult> Handle(GetHappeningsQuery query)
        {
            IEnumerable<Happening> repoResult = happeningRepository.GetAll();
            IEnumerable<HappeningDto> happeningDtos = repoResult.Select(HappeningDtoMapper.HappeningToHappeningDto);

            return new GetHappeningsQueryResult(happeningDtos);
        }
    }
}