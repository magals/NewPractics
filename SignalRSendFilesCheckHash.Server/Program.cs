using SignalRSendFilesCheckHash.Server;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
  var builder = WebApplication.CreateBuilder(args);
  builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .Enrich.WithClientIp()
    .Enrich.WithClientAgent()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341/", apiKey: "TTUfaGlXa8vxOxZ0LqfB"));

  builder.Services.AddHttpContextAccessor();
  builder.Services.AddSignalR()
                  .AddMessagePackProtocol();
  var app = builder.Build();
  app.UseSerilogRequestLogging();
  app.MapHub<SyncHub>("/SyncHub");
  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}