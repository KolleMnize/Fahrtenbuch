using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Application.services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.controllers;

[ApiController]
[Route("[controller]")]
public class CarManagementController(CarManagementService carManagementService) : ControllerBase
{
    [HttpPost(Name = "CreateCar")]
    public async Task<IActionResult> CreateCar(CreateCarCommand command)
    {
        await carManagementService.Handle(command);
        return Ok();
    }

    [HttpGet(Name = "GetCars")]
    public async Task<GetCarsQueryResult> GetCars([FromQuery] GetCarsQuery query)
    {
        return await carManagementService.Handle(query);
    }
}
