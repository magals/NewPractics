using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemplateDefaultAvaloniaUI.ViewModels;
using TemplateDefaultAvaloniaUI.ViewModels.Pages;
using TemplateDefaultAvaloniaUI.Views;
using TemplateDefaultAvaloniaUI.Views.Pages;

namespace TemplateDefaultAvaloniaUI;

public partial class App : Application
{
  public static IHost Host => Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      desktop.MainWindow = new MainWindow
      {
        DataContext = Host.Services.GetService<MainWindowViewModel>()
      };
    }

    base.OnFrameworkInitializationCompleted();
  }

  public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
  {
    var appconfig = new AppConfig();
    host.Configuration.GetSection("AppConfig").Bind(appconfig);

    services.AddSingleton(appconfig);
    services.AddTransient<MainWindow>();
    services.AddTransient<MainWindowViewModel>();

    services.AddTransient<PageWithImageViewModel>();
    services.AddTransient(s =>
    {
      var model = s.GetRequiredService<PageWithImageViewModel>();
      var result = new PageWithImage()
      {
        DataContext = model
      };

      return result;
    });

    services.AddTransient<PageWithSecondImageViewModel>();
    services.AddTransient(s =>
    {
      var model = s.GetRequiredService<PageWithSecondImage>();
      var result = new PageWithImage()
      {
        DataContext = model
      };

      return result;
    });

  }
}