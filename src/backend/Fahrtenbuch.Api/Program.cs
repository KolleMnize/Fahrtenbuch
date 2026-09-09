using Fahrtenbuch.Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

Fahrtenbuch.Application.Installer.DependencyInjection.RegisterApplicationServices(builder.Services, builder.Environment);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializationService = scope.ServiceProvider.GetRequiredService<DbInitializationService>();
    await dbInitializationService.InitializeDatabase();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var response = new { error = "Unexpected error", detail = exception?.Message };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(response);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
