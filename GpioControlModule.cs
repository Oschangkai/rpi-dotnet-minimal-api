using System.Device.Gpio;

public static class GpioControlModule
{
  public static void AddGpioControlEndpoints(this IEndpointRouteBuilder app)
  {
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
  }
}