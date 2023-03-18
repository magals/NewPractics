using SignalRSendFilesCheckHash.Server;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.DependencyInjection;
using SignalRSendFilesCheckHash.Server.Services;
using SignalRSendFilesCheckHash.Models;

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
    .MinimumLevel.Override("Microsoft.AspNetCore.SignalR", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft.AspNetCore.Http.Connections", LogEventLevel.Debug)
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

  AppConfig appconfig = new();
  builder.Configuration.GetSection(nameof(AppConfig)).Bind(appconfig);
  builder.Services.AddSingleton(appconfig);

  builder.Services.AddHttpContextAccessor();
  builder.Services.AddSignalR()
                  .AddMessagePackProtocol();

  builder.Services.AddSingleton<ReadHashContentService>();
  builder.Services.AddHostedService(_ => _.GetRequiredService<ReadHashContentService>());

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