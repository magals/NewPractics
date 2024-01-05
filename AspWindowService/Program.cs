using AspWindowService;
using CliWrap;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Runtime.InteropServices;

const string ServiceName = ".AspWindowService4";

if (args is { Length: 1 })
{
  try
  {
    string executablePath =
        Path.Combine(AppContext.BaseDirectory, "AspWindowService.exe");

    if (args[0] is "/Install")
    {
      await Cli.Wrap("sc")
          .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
          .ExecuteAsync();

      await Cli.Wrap("sc")
         .WithArguments(new[] { "start", ServiceName })
         .ExecuteAsync();

    }
    else if (args[0] is "/Uninstall")
    {
      try
      {
        await Cli.Wrap("sc")
         .WithArguments(new[] { "stop", ServiceName })
         .ExecuteAsync();
      }
      catch (Exception)
      {
      }
     

      await Cli.Wrap("sc")
          .WithArguments(new[] { "delete", ServiceName })
          .ExecuteAsync();
    }


  }
  catch (Exception ex)
  {
    Console.WriteLine(ex);
  }

  return;
}
IConfiguration Configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
   .AddEnvironmentVariables()
   .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
  AppConfig appConfig = new();
  Configuration.Bind(nameof(AppConfig), appConfig);

  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddLogging(builder =>
  {
    var logger = GetConfigsLogger(appConfig).CreateLogger();

    builder.AddSerilog(logger);
  });

  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();
  builder.Services.AddWindowsService();
builder.Services.AddHostedService<ServiceA>();

var app = builder.Build();

  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
  }
  app.UseHttpsRedirection();

  app.UseStaticFiles();

  app.UseRouting();

  app.MapBlazorHub();
  app.MapFallbackToPage("/_Host");

  app.MapRazorPages();
app.MapGet("/", () => "Hello World!");
app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
  Log.Logger.Information("Close Program");
  Log.CloseAndFlush();
}

static LoggerConfiguration GetConfigsLogger(AppConfig appConfig) =>
new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.WithProperty("ApplicationName", Assembly.GetExecutingAssembly().GetName().Name)
                    .Enrich.WithProperty("Version", Assembly.GetExecutingAssembly().GetName().Version)
                    .Enrich.WithProperty("Windows Version", RuntimeInformation.OSDescription)
                    .Enrich.WithProperty("UsedStore", "")
                    .Enrich.WithMemoryUsage()
                    .WriteTo.File(@"logs\logs-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 60)
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithClientIp()
                    .WriteTo.Console();