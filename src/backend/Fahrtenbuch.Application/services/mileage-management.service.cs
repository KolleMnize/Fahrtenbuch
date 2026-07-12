using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.dtos;
using Fahrtenbuch.Application.mapper;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Domain.aggregates;
using Fahrtenbuch.Domain.valueobjects;
using Fahrtenbuch.Infrastructure.services;

namespace Fahrtenbuch.Application.services;

public class MileageManagementService(RepositoryService repositoryService)
{
    public async Task Handle(CreateMileageCommand command)
    {
        var mileage = Mileage.Create(
            MileageId.Create(Guid.NewGuid()).Value,
            CarId.Create(command.CarId).Value,
            command.Value,
            command.Date);
        repositoryService.MileageRepository.Create(mileage);
    }

    public async Task<GetMileagesQueryResult> Handle(GetMileagesQuery query)
    {
        IEnumerable<Mileage> repoResult = repositoryService.MileageRepository.GetAll();
        IEnumerable<MileageDto> mileageDtos = repoResult.Select(MileageDtoMapper.MileageToMileageDto);

        return new GetMileagesQueryResult(mileageDtos);
    }
}