using Fahrtenbuch.Domain.Aggregates;

namespace Fahrtenbuch.Domain.Interfaces.Repositories;

public interface IHappeningRepository
{
    void Create(Happening happening);
    IEnumerable<Happening> GetAll();

}