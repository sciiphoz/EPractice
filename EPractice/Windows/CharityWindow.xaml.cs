using EPractice.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EPractice.Windows
{
    /// <summary>
    /// Логика взаимодействия для CharityWindow.xaml
    /// </summary>
    public partial class CharityWindow : Window
    {
        private Charity _charity;
        public CharityWindow(Charity charity)
        {
            InitializeComponent();

            _charity = charity;

            FundNameTxt.Text = _charity.CharityName;
            FundInfoTxt.Text = _charity.CharityDescription;

            LoadLogo();
        }
        private void LoadLogo()
        {
            try
            {
                if (!string.IsNullOrEmpty(_charity.CharityLogo))
                {
                    string logoPath = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Materials",
                        _charity.CharityLogo);

                    if (System.IO.File.Exists(logoPath))
                    {
                        var logoImage = new BitmapImage(new Uri(logoPath));
                        LogoEllipse.Fill = new ImageBrush(logoImage);
                    }
                    else
                    {
                        var uri = new Uri($"pack://application:,,,/uhebnia_praktika_mubar_321;component/materials/{_fund.CharityLogo}");
                        var logoImage = new BitmapImage(uri);
                        LogoEllipse.Fill = new ImageBrush(logoImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить логотип: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
