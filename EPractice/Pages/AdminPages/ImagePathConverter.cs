using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EPractice.Pages.AdminPages
{
    internal class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = value as string;

            if (!string.IsNullOrEmpty(fileName))
            {
                string fullPath = $"/Images/CharityImages/{fileName}";
                return new BitmapImage(new Uri(fullPath, UriKind.Relative));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
