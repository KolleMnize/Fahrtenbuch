using Fahrtenbuch.Application.commands;
using Fahrtenbuch.Application.querys;
using Fahrtenbuch.Application.services;
using Microsoft.AspNetCore.Mvc;

namespace Fahrtenbuch.Api.controllers;

[ApiController]
[Route("[controller]")]
public class MileageManagementController(MileageManagementService mileageManagementService) : ControllerBase
{
    [HttpPost(Name = "CreateMileage")]
    public async Task<IActionResult> CreateMileage(CreateMileageCommand command)
    {
        await mileageManagementService.Handle(command);
        return Ok();
    }

    [HttpGet(Name = "GetMileage")]
    public async Task<GetMileagesQueryResult> GetMileage([FromQuery] GetMileagesQuery query)
    {
        return await mileageManagementService.Handle(query);
    }
}