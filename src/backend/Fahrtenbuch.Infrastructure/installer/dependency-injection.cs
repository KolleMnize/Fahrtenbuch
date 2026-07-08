using Fahrtenbuch.Infrastructure.persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Fahrtenbuch.Infrastructure.services;

namespace Fahrtenbuch.Infrastructure.installer;

public static class DependencyInjection
{
    public static void RegisterInfrastructureServices(IServiceCollection services, IHostEnvironment environment)
    {
        services.AddDbContext<FahrtenbuchDbContext>(options =>
        {
            options.UseInMemoryDatabase("Fahrtenbuch");
        });

        services.AddSingleton<RepositoryService>();
    }

}
