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
                    case CommandTypes.Add:
                        return Properties.Resources.Add;
                    case CommandTypes.Remove:
                        return Properties.Resources.Remove;
                    case CommandTypes.Update:
                        return Properties.Resources.Update;
                    case CommandTypes.Download:
                        return Properties.Resources.Download;
                    case CommandTypes.Open:
                        return Properties.Resources.OpenFile;
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
