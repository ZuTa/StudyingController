using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using StudyingController.ViewModels;

namespace StudyingController.Converters
{
    public class PictureEnumToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is CommandTypes)
            {
                CommandTypes type = (CommandTypes)value;
                switch (type)
                {
                    case CommandTypes.GeneratePassword:
                        return Properties.Resources.GeneratePassword;
                    case CommandTypes.Lecture:
                        return Properties.Resources.Lecture;
                    case CommandTypes.Practice:
                        return Properties.Resources.Practice;
                    default:
                        throw new NotImplementedException();
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
