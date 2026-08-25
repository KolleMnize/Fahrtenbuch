using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IHappeningRepository
{
    Task Create(Happening happening);
    Task<IEnumerable<Happening>> GetAll();

}