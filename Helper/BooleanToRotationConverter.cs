using System;
using System.Globalization;
using System.Windows.Data;

namespace Keystore_Extractor.Helper
{
    public class BooleanToRotationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 90 : 0; // Rotate 90 degrees when expanded
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
