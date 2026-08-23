using Fahrtenbuch.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Domain.Installer;

public static class DependencyInjection
{
    public static void RegisterDomainServices(IServiceCollection services)
    {
        // Register domain services here
        services.AddScoped<MileageDomainService>();
        services.AddScoped<RideDomainService>();
    }
}