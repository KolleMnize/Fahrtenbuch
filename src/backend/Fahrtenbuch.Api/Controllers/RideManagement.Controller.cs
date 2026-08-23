using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RideManagementController(RideManagementService rideManagementService) : ControllerBase
{
    [HttpPost("CreateRide", Name = "CreateRide")]
    public async Task<IActionResult> CreateRide(CreateRideCommand command)
    {
        var result = await rideManagementService.Handle(command);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
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
    public async Task<GetRidesQueryResult> GetRides([FromQuery] GetRidesQuery query)
    {
        return await rideManagementService.Handle(query);
    }
}
