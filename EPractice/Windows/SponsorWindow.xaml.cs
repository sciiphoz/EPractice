using EPractice.Pages.SponsorPages;
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
    /// Логика взаимодействия для SponsorWindow.xaml
    /// </summary>
    public partial class SponsorWindow : Window
    {
        private int page;
        public SponsorWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SponsorPage());
            page = 0;
        }
        public void ConfirmSponsor()
        {
            MainFrame.Navigate(new ConfirmSponsorPage());
            page = 1;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            switch (page)
            {
                case 0:
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                    break;
                case 1:
                    MainFrame.Navigate(new SponsorPage());
                    page = 0;
                    break;
                default:
                    break;
            }

        }
    }
}
