using Fahrtenbuch.Domain.Cars;
using Fahrtenbuch.Domain.Happenings;
using Fahrtenbuch.Domain.Mileages;
using Fahrtenbuch.Domain.Rides;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Domain.Installer;

public static class DependencyInjection
{
    public static void RegisterDomainServices(IServiceCollection services)
    {
        // Register domain services here
        services.AddScoped<MileageDomainService>();
        services.AddScoped<RideDomainService>();
        services.AddScoped<IRideDeletionService, RideDeletionService>();
        services.AddScoped<IHappeningDeletionService, HappeningDeletionService>();
        services.AddScoped<ICarDeletionService, CarDeletionService>();
        services.AddScoped<IMileageDeletionService, MileageDeletionService>();
    }
}