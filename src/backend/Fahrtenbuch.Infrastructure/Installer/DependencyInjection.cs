using Fahrtenbuch.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fahrtenbuch.Infrastructure.Persistence.Repositories;
using Fahrtenbuch.Domain.Interfaces.Repositories;

namespace Fahrtenbuch.Infrastructure.Installer;

public static class DependencyInjection
{
    public static void RegisterInfrastructureServices(IServiceCollection services, IHostEnvironment environment)
    {
        services.AddDbContext<FahrtenbuchDbContext>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IMileageRepository, MileageRepository>();
        services.AddScoped<HappeningRepository, HappeningRepository>();
    }

}
