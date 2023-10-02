
using CliWrap;
using MudBlazor.Services;
using Serilog;
using TemplateBackgroundService;
using TemplateBackgroundService.Models;

const string ServiceName = ".TemplateBackgroundService";
if (args is { Length: 1 })
{
  try
  {
    string executablePath =
        Path.Combine(AppContext.BaseDirectory, "TemplateBackgroundService.exe");

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
      await Cli.Wrap("sc")
          .WithArguments(new[] { "stop", ServiceName })
          .ExecuteAsync();

      await Cli.Wrap("sc")
          .WithArguments(new[] { "delete", ServiceName })
          .ExecuteAsync();
    }
    else if (args[0] is "/drop")
    {
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
  Log.Logger.Information("Start Program");

  AppConfig appConfig = new();
  Configuration.Bind(nameof(AppConfig), appConfig);
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddLogging(builder => builder.AddProvider(new CustomLoggerProvider()));
  builder.Services.AddSingleton(appConfig);

  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();
  builder.Services.AddMudServices();

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