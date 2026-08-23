using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories
{
    public class HappeningRepository(FahrtenbuchDbContext dbContext) : IHappeningRepository
    {
        public void Create(Happening happening)
        {
            // var scope = serviceProvider.CreateScope();
            // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

            dbContext.Set<Happening>().Add(happening);
            dbContext.SaveChanges();
        }

        public IEnumerable<Happening> GetAll()
        {
            // var scope = serviceProvider.CreateScope();
            // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

            return dbContext.Set<Happening>().ToList();
        }
    }
}