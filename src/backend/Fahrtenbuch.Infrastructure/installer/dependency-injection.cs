using Fahrtenbuch.Infrastructure.persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fahrtenbuch.Infrastructure.persistence.repositories;
using Fahrtenbuch.Domain.interfaces.repositories;

namespace Fahrtenbuch.Infrastructure.installer;

public static class DependencyInjection
{
    public static void RegisterInfrastructureServices(IServiceCollection services, IHostEnvironment environment)
    {
        services.AddDbContext<FahrtenbuchDbContext>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IMileageRepository, MileageRepository>();
    }

}
