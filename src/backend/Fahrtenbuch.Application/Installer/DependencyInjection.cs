using Fahrtenbuch.Application.Features.Cars.CreateCar;
using Fahrtenbuch.Application.Services;
using Fahrtenbuch.Application.Features.Cars.GetCars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fahrtenbuch.Application.Installer;

public static class DependencyInjection
{
    public static void RegisterApplicationServices(IServiceCollection services, IHostEnvironment environment)
    {
        Infrastructure.Installer.DependencyInjection.RegisterInfrastructureServices(services, environment);
        Domain.Installer.DependencyInjection.RegisterDomainServices(services);
        services.AddScoped<CreateCarHandler>();
        services.AddScoped<GetCarsHandler>();
        services.AddScoped<MileageManagementService>();
        services.AddScoped<HappeningManagementService>();
        services.AddScoped<RideManagementService>();
    }

}
