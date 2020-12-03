using System;
using System.Globalization;
using Xamarin.Forms;

namespace StringConverter.Converters
{
    public class BooleanToStringConverter : IValueConverter
    {

        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Text1 : Text2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
