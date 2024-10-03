using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Keystore_Extractor.Helper
{
    public class FilenameToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string filepath = value as string;
            return string.IsNullOrEmpty(filepath) ? "Keystore:" : $"Keystore: {Path.GetFileNameWithoutExtension(filepath)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
