using Iot.Device.CpuTemperature;

public static class HealthModule
{
  public static void AddHealthEndpoint(this IEndpointRouteBuilder app)
  {
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
  }
}
public record SystemHealth(double Temperature);