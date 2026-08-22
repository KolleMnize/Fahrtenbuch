using Microsoft.Extensions.DependencyInjection;
using Fahrtenbuch.Infrastructure.Persistence;

namespace Fahrtenbuch.Infrastructure.Services;

public class DbInitializationService(IServiceProvider serviceProvider)
{
    public void InitializeDatabase()
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var dbSeederService = scope.ServiceProvider.GetRequiredService<DbSeederService>();
            dbSeederService.Seed();
        }
    }
}
