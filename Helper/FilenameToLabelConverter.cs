using System;
using System.Globalization;
using System.Windows.Data;

namespace Keystore_Extractor.Helper
{
    public class FilenameToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string filename = value as string;
            return string.IsNullOrEmpty(filename) ? "Keystore" : $"Keystore {filename}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
