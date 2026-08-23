using Fahrtenbuch.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fahrtenbuch.Application.Installer;

public static class DependencyInjection
{
    public static void RegisterApplicationServices(IServiceCollection services, IHostEnvironment environment)
    {
        Infrastructure.Installer.DependencyInjection.RegisterInfrastructureServices(services, environment);
        Domain.Installer.DependencyInjection.RegisterDomainServices(services);
        services.AddScoped<CarManagementService>();
        services.AddScoped<MileageManagementService>();
        services.AddScoped<HappeningManagementService>();
        services.AddScoped<RideManagementService>();
    }

}
