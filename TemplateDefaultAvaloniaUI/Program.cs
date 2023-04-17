using Avalonia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TemplateDefaultAvaloniaUI
{
  internal class Program
  {
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();

    public static IHostBuilder CreateHostBuilder(string[] Args)
    {
      return Host.CreateDefaultBuilder(Args)
          .ConfigureAppConfiguration((host, cfg) => cfg
              .AddJsonFile("appsettings.json", true, true)
              )
          .ConfigureServices(App.ConfigureServices);
    }
  }
}
