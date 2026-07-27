using Fahrtenbuch.Domain.services;
using Microsoft.Extensions.DependencyInjection;

namespace Fahrtenbuch.Domain.installer;

public static class DependencyInjection
{
    public static void RegisterDomainServices(IServiceCollection services)
    {
        // Register domain services here
        services.AddScoped<MileageDomainService>();
    }
}