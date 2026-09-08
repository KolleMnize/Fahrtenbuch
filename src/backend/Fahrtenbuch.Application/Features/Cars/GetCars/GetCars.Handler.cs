using Fahrtenbuch.Application.Interfaces;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Fahrtenbuch.Domain.Aggregates;
using ErrorOr;

namespace Fahrtenbuch.Application.Features.Cars.GetCars;

public class GetCarsHandler(ICarRepository carRepository) : IQueryHandler<GetCarsQuery, GetCarsQueryResult>
{
    public async Task<ErrorOr<GetCarsQueryResult>> Handle(GetCarsQuery query, CancellationToken ct = default)
    {
        ErrorOr<IReadOnlyList<Car>> repoResult = await carRepository.GetAll(ct);
        if (repoResult.IsError)
            return repoResult.Errors;

        IReadOnlyList<CarDto> carDtos = repoResult.Value.Select(CarDtoMapper.CarToCarDto).ToList();

        return new GetCarsQueryResult(carDtos);
    }
}