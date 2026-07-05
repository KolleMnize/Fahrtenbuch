using Fahrtenbuch.Application.commands;
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

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
