using System.Globalization;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
  .WithTracing(b =>
  {
    b
    .AddConsoleExporter()
    .AddSource(serviceName)
    .ConfigureResource(resource =>
        resource.AddService(
          serviceName: serviceName,
          serviceVersion: serviceVersion));
  });
var app = builder.Build();

var logger = app.Logger;

int RollDice()
{
  return Random.Shared.Next(1, 7);
}

string HandleRollDice(string? player)
{
  var result = RollDice();

  if (string.IsNullOrEmpty(player))
  {
    logger.LogInformation("Anonymous player is rolling the dice: {result}", result);
  }
  else
  {
    logger.LogInformation("{player} is rolling the dice: {result}", player, result);
  }

  return result.ToString(CultureInfo.InvariantCulture);
}

app.MapGet("/rolldice/{player?}", HandleRollDice);

app.Run();