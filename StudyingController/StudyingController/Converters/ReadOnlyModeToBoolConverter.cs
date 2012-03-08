using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using StudyingController.ViewModels;

namespace StudyingController.Converters
{
    public class ReadOnlyModeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is EditModes)
                switch ((EditModes)value)
                {
                    case EditModes.ReadOnly:
                        return true;
                    case EditModes.Editable:
                        return false;
                    default:
                        return false;
                }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
