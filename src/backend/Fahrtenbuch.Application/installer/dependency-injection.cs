using Fahrtenbuch.Application.services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fahrtenbuch.Application.installer;

public static class DependencyInjection
{
    public static void RegisterApplicationServices(IServiceCollection services, IHostEnvironment environment)
    {
        Infrastructure.installer.DependencyInjection.RegisterInfrastructureServices(services, environment);
        services.AddScoped<DbInitializationService>();
        Domain.installer.DependencyInjection.RegisterDomainServices(services);
        services.AddScoped<CarManagementService>();
        services.AddScoped<MileageManagementService>();
    }

}
