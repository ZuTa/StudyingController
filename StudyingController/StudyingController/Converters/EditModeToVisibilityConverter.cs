using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using StudyingController.ViewModels;
using System.Windows;

namespace StudyingController.Converters
{
    public class EditModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EditModes)
                switch ((EditModes)value)
                {
                    case EditModes.Editable:
                        return Visibility.Visible;
                    case EditModes.ReadOnly:
                        return Visibility.Collapsed;
                    default:
                        return Visibility.Collapsed;
                }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
