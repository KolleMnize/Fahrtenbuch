using Fahrtenbuch.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fahrtenbuch.Infrastructure.Services;
using Fahrtenbuch.Domain.Cars;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.Happenings;
using Fahrtenbuch.Domain.Rides;
using Fahrtenbuch.Infrastructure.Persistence.Cars;
using Fahrtenbuch.Infrastructure.Persistence.Mileages;
using Fahrtenbuch.Infrastructure.Persistence.Happenings;
using Fahrtenbuch.Infrastructure.Persistence.Rides;

namespace Fahrtenbuch.Infrastructure.Installer;

public static class DependencyInjection
{
    public static void RegisterInfrastructureServices(IServiceCollection services, IHostEnvironment environment)
    {
        services.AddScoped<DbInitializationService>();
        services.AddDbContext<FahrtenbuchDbContext>();
        services.AddScoped<DbSeederService>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IMileageRepository, MileageRepository>();
        services.AddScoped<IHappeningRepository, HappeningRepository>();
        services.AddScoped<IRideRepository, RideRepository>();
    }

}
