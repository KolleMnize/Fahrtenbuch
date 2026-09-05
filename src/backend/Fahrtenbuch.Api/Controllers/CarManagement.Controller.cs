using Fahrtenbuch.Api.Extensions;
using Fahrtenbuch.Application.Features.Cars.CreateCar;
using Fahrtenbuch.Application.Features.Cars.GetCars;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CarManagementController(CreateCarHandler createCarHandler, GetCarsHandler getCarsHandler) : ControllerBase
{
    [HttpPost("CreateCar", Name = "CreateCar")]
    public async Task<IActionResult> CreateCar(CreateCarCommand command)
    {
        var result = await createCarHandler.Handle(command);
        return result.ToProblemDetails(this);
    }

    [HttpGet("GetCars", Name = "GetCars")]
    public async Task<IActionResult> GetCars([FromQuery] GetCarsQuery query)
    {
        var result = await getCarsHandler.Handle(query);
        return result.ToProblemDetails(this);
    }
}
