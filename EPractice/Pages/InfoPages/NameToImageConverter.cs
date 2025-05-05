using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace EPractice.Pages.InfoPages
{
    public class NameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            string name = value.ToString().ToLower().Replace(" ", "-");
            try
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/ComparisonImages/{name}.jpg"));
            }
            catch
            {
                return new BitmapImage(new Uri("pack://application:,,,/Images/ComparisonImages/default.jpg"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
