using Microsoft.Extensions.DependencyInjection;
using Fahrtenbuch.Infrastructure.Persistence;

namespace Fahrtenbuch.Application.Services;

public class DbInitializationService(IServiceProvider serviceProvider)
{
    public void InitializeDatabase()
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<FahrtenbuchDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
