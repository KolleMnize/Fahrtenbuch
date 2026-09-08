using Fahrtenbuch.Application.Features.Cars.CreateCar;
using Fahrtenbuch.Application.Services;
using Fahrtenbuch.Application.Features.Mileages.GetMileages;
using Fahrtenbuch.Application.Features.Cars.GetCars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fahrtenbuch.Application.Features.Mileages.CreateMileage;

namespace Fahrtenbuch.Application.Installer;

public static class DependencyInjection
{
    public static void RegisterApplicationServices(IServiceCollection services, IHostEnvironment environment)
    {
        Infrastructure.Installer.DependencyInjection.RegisterInfrastructureServices(services, environment);
        Domain.Installer.DependencyInjection.RegisterDomainServices(services);

        services.AddScoped<CreateCarHandler>();
        services.AddScoped<GetCarsHandler>();

        services.AddScoped<CreateMileageHandler>();
        services.AddScoped<GetMileagesHandler>();

        services.AddScoped<HappeningManagementService>();
        services.AddScoped<RideManagementService>();
    }

}
