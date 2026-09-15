using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Features.Happenings.CreateHappening;
using Fahrtenbuch.Application.Features.Happenings.DeleteHappening;
using Fahrtenbuch.Application.Features.Happenings.GetHappenings;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HappeningManagementController(CreateHappeningHandler createHappeningHandler, GetHappeningsHandler getHappeningsHandler, DeleteHappeningHandler deleteHappeningHandler) : ControllerBase
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

    [HttpDelete("DeleteHappening", Name = "DeleteHappening")]
    public async Task<IActionResult> DeleteHappening([FromQuery] DeleteHappeningCommand command)
    {
        var result = await deleteHappeningHandler.Handle(command);
        return result.ToProblemDetails(this);
    }
}
