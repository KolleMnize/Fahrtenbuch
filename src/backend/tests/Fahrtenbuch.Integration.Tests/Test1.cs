using System.Net.Http.Json;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Querys;

namespace Fahrtenbuch.Integration.Tests;

[TestClass]
public sealed class RideEndpointsTests
{
    private static FahrtenbuchWebApplicationFactory _factory = null!;
    private static HttpClient _client = null!;

    // Läuft EINMAL vor allen Tests dieser Klasse
    [ClassInitialize]
    public static void ClassInit(TestContext context)
    {
        _factory = new FahrtenbuchWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    // Läuft EINMAL nach allen Tests dieser Klasse
    [ClassCleanup]
    public static void ClassCleanup()
    {
        _factory.Dispose();
    }

    [TestMethod]
    public async Task StartRide_Should_Return_Created()
    {
        // Arrange
        EndRideCommand command = new EndRideCommand
        (Guid.Parse("82473fc2-9ee5-4670-834c-7c8401ec2df1"), Guid.Parse("42473fc2-9ee5-4670-834c-7c8401ec2df1"));

        // Act
        var response = await _client.PutAsJsonAsync("/RideManagement/EndRide", command);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(
            response.IsSuccessStatusCode,
            $"Status: {(int)response.StatusCode} {response.StatusCode}, Body: {content}"
        );

        GetRidesQueryResult rides = await _client.GetFromJsonAsync<GetRidesQueryResult>("/RideManagement/GetRides") ?? throw new InvalidOperationException("Response body is null");

        Assert.IsTrue(rides.Rides.Any(r => r.RideId == command.RideId && r.EndMileageId == command.EndMileageId),
            $"Ride with RideId {command.RideId} and EndMileageId {command.EndMileageId} not found in response.");

    }
}
