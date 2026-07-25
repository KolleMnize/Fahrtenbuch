using Microsoft.Extensions.DependencyInjection;
using Fahrtenbuch.Infrastructure.persistence;

namespace Fahrtenbuch.Application.services;

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
