using System;
using System.Globalization;
using System.Windows.Data;

namespace Myth.FileSplitSender.View.FileSplitter
{
    class ProcessValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleValue = (double) value;
            return (doubleValue*100).ToString("##0.0") + "%";
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
