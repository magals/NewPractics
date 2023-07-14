using AspWindowService;
using CliWrap;

const string ServiceName = ".AspWindowService";

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
      await Cli.Wrap("sc")
          .WithArguments(new[] { "stop", ServiceName })
          .ExecuteAsync();

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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddWindowsService();
builder.Services.AddHostedService<ServiceA>();

var app = builder.Build();

app.MapRazorPages();
app.MapGet("/", () => "Hello World!");
app.Run();
