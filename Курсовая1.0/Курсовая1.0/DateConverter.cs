using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Курсовая1._0
{
    public class DateToBrushConverter : IValueConverter
    {
        private static readonly SolidColorBrush OverdueBrush =
            new SolidColorBrush(Color.FromRgb(255, 200, 200));
        private static readonly SolidColorBrush DefaultBrush = Brushes.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;
            DateTime.TryParse(value as string, out date);
            return date < DateTime.Now ? OverdueBrush : DefaultBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}