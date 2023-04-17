using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using TemplateDefaultAvaloniaUI.ViewModels.Pages;

namespace TemplateDefaultAvaloniaUI.ViewModels;

partial class MainWindowViewModel : ObservableObject
{
  private readonly ILogger<MainWindowViewModel> logger;
  private readonly PageWithImageViewModel pageWithImageViewModel;
  [ObservableProperty]
  private string greeting = "Welcome to Avalonia!";

  public MainWindowViewModel(ILogger<MainWindowViewModel> logger,
                             PageWithImageViewModel pageWithImageViewModel)
  {
    this.logger = logger;
    this.pageWithImageViewModel = pageWithImageViewModel;
    logger.LogInformation("MainWindowViewModel Show");
  }
}
