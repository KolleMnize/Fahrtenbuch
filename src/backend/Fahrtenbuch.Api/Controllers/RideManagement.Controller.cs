using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Features.Rides.CreateRide;
using Fahrtenbuch.Application.Features.Rides.GetRides;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RideManagementController(CreateRideHandler createRideCommandHandler, GetRidesHandler getRidesHandler, RideManagementService rideManagementService) : ControllerBase
{
    [HttpPost("CreateRide", Name = "CreateRide")]
    public async Task<IActionResult> CreateRide(CreateRideCommand command)
    {
        var result = await createRideCommandHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpPut("EndRide", Name = "EndRide")]
    public async Task<IActionResult> EndRide(EndRideCommand command)
    {
        var result = await rideManagementService.Handle(command);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpGet("GetRides", Name = "GetRides")]
    public async Task<IActionResult> GetRides([FromQuery] GetRidesQuery query)
    {
        var result = await getRidesHandler.Handle(query);
        return result.ToProblemDetails(this);
    }
}
