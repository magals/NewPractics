using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using TemplateDefaultAvaloniaUI.Models.Enums;
using TemplateDefaultAvaloniaUI.ViewModels.Pages;

namespace TemplateDefaultAvaloniaUI.ViewModels;

partial class MainWindowViewModel : ObservableObject
{
  private readonly ILogger<MainWindowViewModel> logger;
  public PageWithImageViewModel PageWithImageViewModel { get; }
  public PageWithSecondImageViewModel PageWithSecondImageViewModel { get; }

  [ObservableProperty]
  private string greeting = "Welcome to Avalonia!";

  [ObservableProperty]
  private ChooseImage chooseImage = ChooseImage.None;

  public MainWindowViewModel(ILogger<MainWindowViewModel> logger,
                             PageWithImageViewModel pageWithImageViewModel,
                             PageWithSecondImageViewModel pageWithSecondImageViewModel)
  {
    this.logger = logger;
    this.PageWithImageViewModel = pageWithImageViewModel;
    PageWithSecondImageViewModel = pageWithSecondImageViewModel;
    logger.LogInformation("MainWindowViewModel Show");
  }

  #region Commands
  [RelayCommand]
  private void ChooseImagesCommand(ChooseImage p)
  {
    logger.LogInformation("ChooseImagesCommand:{0}",p);
    ChooseImage = p;
  }
  #endregion
}
