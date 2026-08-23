using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HappeningManagementController(HappeningManagementService happeningManagementService) : ControllerBase
{
    [HttpPost("CreateHappening", Name = "CreateHappening")]
    public async Task<IActionResult> CreateHappening(CreateHappeningCommand command)
    {
        var result = await happeningManagementService.Handle(command);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpGet("GetHappenings", Name = "GetHappenings")]
    public async Task<GetHappeningsQueryResult> GetHappenings([FromQuery] GetHappeningsQuery query)
    {
        return await happeningManagementService.Handle(query);
    }
}
