using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using StudyingController.ViewModels;

namespace StudyingController.Converters
{
    class PictureEnumToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch((int)((CommandTypes)value))
            {
                case 1:
                    return Properties.Resources.GeneratePassword;
                case 2:
                    return Properties.Resources.Lecture;
                case 3:
                    return Properties.Resources.Practice;
                default:
                    break;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
