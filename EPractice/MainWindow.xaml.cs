using EPractice.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EPractice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SponsorButton_Click(object sender, RoutedEventArgs e)
        {
            SponsorWindow sponsorWindow = new SponsorWindow();
            sponsorWindow.Show();
            Close();
        }

        private void RunnerButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerWindow runnerWindow = new RunnerWindow();
            runnerWindow.Show();
            Close();
        }
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
            Close();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
