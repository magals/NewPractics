using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDefaultAvaloniaUI.ViewModels.Pages;

partial class PageWithImageViewModel : ObservableObject
{
  private readonly ILogger<PageWithImageViewModel> logger;

  public PageWithImageViewModel(ILogger<PageWithImageViewModel> logger)
  {
    this.logger = logger;
  }
}
