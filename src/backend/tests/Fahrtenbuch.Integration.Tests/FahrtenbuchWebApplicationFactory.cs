using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fahrtenbuch.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Fahrtenbuch.Integration.Tests
{
    public class FahrtenbuchWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Entfernt die "echte" DbContext-Registrierung
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FahrtenbuchDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                // Registriert eine frische InMemory-DB pro Testlauf (eindeutiger Name!)
                services.AddDbContext<FahrtenbuchDbContext>(options =>
                {
                    options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}");
                });
            });
        }
    }
}