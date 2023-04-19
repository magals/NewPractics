using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateDefaultAvaloniaUI.ViewModels.Pages
{
  partial class PageWithSecondImageViewModel : ObservableObject
  {
    private readonly ILogger<PageWithSecondImageViewModel> logger;

    public PageWithSecondImageViewModel(ILogger<PageWithSecondImageViewModel> logger)
    {
      this.logger = logger;
    }
  }
}
