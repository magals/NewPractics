using Avalonia.Data.Converters;
using System.Globalization;
using TemplateDefaultAvaloniaUI.Models.Enums;

namespace TemplateDefaultAvaloniaUI.Infostructure.Converts;

internal class EnumTabPagetoIndexConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {

    if (value == null || parameter == null) return false;
    if (value is bool) return false;

    if (value is ViewStates viewStates)
    {
      ViewStates viewType = (ViewStates)parameter;

      return viewStates == viewType;
    }

    if (value is MainViewState mainViewState)
    {
      MainViewState viewType = (MainViewState)parameter;

      return mainViewState == viewType;
    }

    return false;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null || parameter == null) return false;

    bool isSelected = (bool)value;
    if (!isSelected) return false;

    ViewStates vTestType = (ViewStates)parameter;

    return vTestType;
  }
}
