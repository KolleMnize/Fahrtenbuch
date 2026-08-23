using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MileageManagementController(MileageManagementService mileageManagementService) : ControllerBase
{
    [HttpPost("CreateMileage", Name = "CreateMileage")]
    public async Task<IActionResult> CreateMileage(CreateMileageCommand command)
    {
        var result = await mileageManagementService.Handle(command);
        if (result.IsError)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpGet("GetMileage", Name = "GetMileage")]
    public async Task<GetMileagesQueryResult> GetMileage([FromQuery] GetMileagesQuery query)
    {
        return await mileageManagementService.Handle(query);
    }
}