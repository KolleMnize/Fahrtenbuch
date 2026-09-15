using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Features.Rides.CreateRide;
using Fahrtenbuch.Application.Features.Rides.DeleteRide;
using Fahrtenbuch.Application.Features.Rides.EndRide;
using Fahrtenbuch.Application.Features.Rides.GetRides;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RideManagementController(CreateRideHandler createRideCommandHandler, GetRidesHandler getRidesHandler, EndRideHandler endRideHandler, DeleteRideHandler deleteRideHandler) : ControllerBase
{
    [HttpPost("CreateRide", Name = "CreateRide")]
    public async Task<IActionResult> CreateRide([FromQuery] CreateRideCommand command)
    {
        var result = await createRideCommandHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpPut("EndRide", Name = "EndRide")]
    public async Task<IActionResult> EndRide([FromQuery] EndRideCommand command)
    {
        var result = await endRideHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpGet("GetRides", Name = "GetRides")]
    public async Task<IActionResult> GetRides([FromQuery] GetRidesQuery query)
    {
        var result = await getRidesHandler.Handle(query);
        return result.ToProblemDetails(this);
    }

    [HttpDelete("DeleteRide", Name = "DeleteRide")]
    public async Task<IActionResult> DeleteRide([FromQuery] DeleteRideCommand command)
    {
        var result = await deleteRideHandler.Handle(command);
        return result.ToProblemDetails(this);
    }
}
