using Fahrtenbuch.Application.commands;

namespace Fahrtenbuch.Application.services;

public class CarManagementService
{
    public async Task Handle(CreateCarCommand command)
    {
        // Implement the logic to handle the creation of a car here
        // For example, you might want to validate the command and then save it to a database
    }
}