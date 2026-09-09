using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Features.Happenings.CreateHappening;
using Fahrtenbuch.Application.Features.Happenings.GetHappenings;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HappeningManagementController(CreateHappeningHandler createHappeningHandler, GetHappeningsHandler getHappeningsHandler) : ControllerBase
{
    [HttpPost("CreateHappening", Name = "CreateHappening")]
    public async Task<IActionResult> CreateHappening(CreateHappeningCommand command)
    {
        var result = await createHappeningHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpGet("GetHappenings", Name = "GetHappenings")]
    public async Task<IActionResult> GetHappenings([FromQuery] GetHappeningsQuery query)
    {
        var result = await getHappeningsHandler.Handle(query);
        return result.ToProblemDetails(this);
    }
}
