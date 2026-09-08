using Fahrtenbuch.Application.Interfaces;

namespace Fahrtenbuch.Application.Features.Mileages.GetMileages;

public record GetMileagesQuery() : IQuery<GetMileagesQueryResult>;
