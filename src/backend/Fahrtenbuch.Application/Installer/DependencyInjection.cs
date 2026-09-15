using Fahrtenbuch.Application.Features.Cars.CreateCar;
using Fahrtenbuch.Application.Features.Mileages.GetMileages;
using Fahrtenbuch.Application.Features.Cars.GetCars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fahrtenbuch.Application.Features.Mileages.CreateMileage;
using Fahrtenbuch.Application.Features.Happenings.CreateHappening;
using Fahrtenbuch.Application.Features.Happenings.GetHappenings;
using Fahrtenbuch.Application.Features.Rides.CreateRide;
using Fahrtenbuch.Application.Features.Rides.GetRides;
using Fahrtenbuch.Application.Features.Rides.EndRide;
using Fahrtenbuch.Application.Features.Rides.DeleteRide;
using Fahrtenbuch.Application.Features.Happenings.DeleteHappening;
using Fahrtenbuch.Application.Features.Cars.DeleteCar;
using Fahrtenbuch.Application.Features.Mileages.DeleteMilegae;

namespace Fahrtenbuch.Application.Installer;

public static class DependencyInjection
{
    public static void RegisterApplicationServices(IServiceCollection services, IHostEnvironment environment)
    {
        Infrastructure.Installer.DependencyInjection.RegisterInfrastructureServices(services, environment);
        Domain.Installer.DependencyInjection.RegisterDomainServices(services);

        services.AddScoped<CreateCarHandler>();
        services.AddScoped<GetCarsHandler>();
        services.AddScoped<DeleteCarHandler>();

        services.AddScoped<CreateMileageHandler>();
        services.AddScoped<GetMileagesHandler>();
        services.AddScoped<DeleteMileageHandler>();

        services.AddScoped<CreateHappeningHandler>();
        services.AddScoped<GetHappeningsHandler>();
        services.AddScoped<DeleteHappeningHandler>();

        services.AddScoped<CreateRideHandler>();
        services.AddScoped<GetRidesHandler>();
        services.AddScoped<EndRideHandler>();
        services.AddScoped<DeleteRideHandler>();
    }

}
