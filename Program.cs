using System.Device.Gpio;
using System.Text.Json.Serialization;
using Iot.Device.CpuTemperature;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapGet("/ping", () => "pong");

var healthApi = app.MapGroup("/health");
healthApi.MapGet("/temperature", () =>
{
    using CpuTemperature cpuTemperature = new();
    if (!cpuTemperature.IsAvailable)
    {
        return Results.Problem("CPU temperature is not available");
    }

    return Results.Ok(new SystemHealth(cpuTemperature.Temperature.DegreesCelsius));
});

var doorApi = app.MapGroup("/door");
doorApi.MapGet("/open", () =>
{
    const int RELAY = 23;
    using var controller = new GpioController();
    controller.OpenPin(RELAY, PinMode.Output);
    for (int i = 0; i < 2; i++)
    {
        controller.Write(RELAY, PinValue.High);
        Thread.Sleep(250);
        controller.Write(RELAY, PinValue.Low);
        Thread.Sleep(250);
    }
    return Results.Ok();
});

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);
public record SystemHealth(double Temperature);

[JsonSerializable(typeof(SystemHealth))]
[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
