using Fahrtenbuch.Domain.Aggregates;
using Fahrtenbuch.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Infrastructure.Persistence.Repositories
{
    public class HappeningRepository(FahrtenbuchDbContext dbContext) : IHappeningRepository
    {
        public async Task Create(Happening happening)
        {
            // var scope = serviceProvider.CreateScope();
            // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

            dbContext.Set<Happening>().Add(happening);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Happening>> GetAll()
        {
            // var scope = serviceProvider.CreateScope();
            // var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();

            return await dbContext.Set<Happening>().ToListAsync();
        }
    }
}