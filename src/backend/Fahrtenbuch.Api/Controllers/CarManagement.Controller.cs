using Fahrtenbuch.Application.Commands;
using Fahrtenbuch.Application.Querys;
using Fahrtenbuch.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CarManagementController(CarManagementService carManagementService) : ControllerBase
{
    [HttpPost("CreateCar", Name = "CreateCar")]
    public async Task<IActionResult> CreateCar(CreateCarCommand command)
    {
        await carManagementService.Handle(command);
        return Ok();
    }

    [HttpGet("GetCars", Name = "GetCars")]
    public async Task<GetCarsQueryResult> GetCars([FromQuery] GetCarsQuery query)
    {
        return await carManagementService.Handle(query);
    }
}
