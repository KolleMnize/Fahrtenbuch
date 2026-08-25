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
        public async Task<ErrorOr<Happening>> Handle(CreateHappeningCommand command)
        {
            var mileageExists = await mileageRepository.Exists(MileageId.Create(command.MileageId).Value);
            if (!mileageExists)
                return Error.Validation(code: "MileageNotFound", description: $"Mileage with id {command.MileageId} does not exist.");

            var happeningCreateResult = Happening.Create(
                HappeningId.Create(Guid.NewGuid()).Value,
                command.Description,
                MileageId.Create(command.MileageId).Value);

            if (happeningCreateResult.IsError)
            {
                return happeningCreateResult.Errors;
            }

            await happeningRepository.Create(happeningCreateResult.Value);

            return happeningCreateResult.Value;
        }

        public async Task<GetHappeningsQueryResult> Handle(GetHappeningsQuery query)
        {
            IEnumerable<Happening> repoResult = await happeningRepository.GetAll();
            IEnumerable<HappeningDto> happeningDtos = repoResult.Select(HappeningDtoMapper.HappeningToHappeningDto);

            return new GetHappeningsQueryResult(happeningDtos);
        }
    }
}