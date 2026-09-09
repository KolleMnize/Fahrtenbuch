using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Features.Mileages.CreateMileage;
using Fahrtenbuch.Application.Features.Mileages.GetMileages;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MileageManagementController(
    CreateMileageHandler createMileageHandler,
    GetMileagesHandler getMileagesHandler) : ControllerBase
{
    [HttpPost("CreateMileage", Name = "CreateMileage")]
    public async Task<IActionResult> CreateMileage(CreateMileageCommand command)
    {
        var result = await createMileageHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpGet("GetMileage", Name = "GetMileage")]
    public async Task<IActionResult> GetMileage([FromQuery] GetMileagesQuery query)
    {
        var result = await getMileagesHandler.Handle(query);
        return result.ToProblemDetails(this);
    }
}